using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonVisual : MonoBehaviour
{
    private EventSystem _eventSystem;

    private Animator _animator;

    private bool _isLastPressed = false;

    private readonly int IS_PRESSED = Animator.StringToHash("IsPressed");

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _eventSystem.OnResetButtonClick += ResetVisual;
        _eventSystem.OnPlayerMovement += SaveState;
        _eventSystem.OnUndoButtonClick += Undo;
    }

    private void OnDisable()
    {
        _eventSystem.OnResetButtonClick -= ResetVisual;
        _eventSystem.OnPlayerMovement -= SaveState;
        _eventSystem.OnUndoButtonClick -= Undo;
    }

    public void PressButton() => _animator.SetBool(IS_PRESSED, true);

    private void ResetVisual() => _animator.SetBool(IS_PRESSED, false);

    private void SaveState() => _isLastPressed = _animator.GetBool(IS_PRESSED);

    private void Undo() => _animator.SetBool(IS_PRESSED, _isLastPressed);
}
