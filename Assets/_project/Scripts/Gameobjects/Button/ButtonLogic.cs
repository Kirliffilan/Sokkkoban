using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonLogic : MonoBehaviour
{
    private EventSystem _eventSystem;

    private Animator _animator;

    private bool _isLastPressed = false;

    public bool IsPressed { get; private set; } = false;

    private readonly int IS_PRESSED = Animator.StringToHash("IsPressed");

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _eventSystem.OnResetButtonClick += ResetButton;
        _eventSystem.OnPlayerMovement += SaveState;
        _eventSystem.OnUndoButtonClick += Undo;
    }

    private void OnDisable()
    {
        _eventSystem.OnResetButtonClick -= ResetButton;
        _eventSystem.OnPlayerMovement -= SaveState;
        _eventSystem.OnUndoButtonClick -= Undo;
    }

    public void PressButton()
    {
        IsPressed = true;
        _eventSystem.ButtonPressed();
        _animator.SetBool(IS_PRESSED, true);
    }

    private void ResetButton()
    {
        _animator.SetBool(IS_PRESSED, false);
        IsPressed = false;
    }
    private void SaveState() => _isLastPressed = IsPressed;

    private void Undo()
    {
        _animator.SetBool(IS_PRESSED, _isLastPressed);
        IsPressed = _isLastPressed;
    }
}
