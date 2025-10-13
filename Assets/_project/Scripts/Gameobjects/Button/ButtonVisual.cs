using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonVisual : MonoBehaviour
{
    private EventSystem _eventSystem;

    private Animator _animator;

    private readonly int IS_PRESSED = Animator.StringToHash("IsPressed");

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _eventSystem.OnResetButtonClick += ResetVisual;
    }

    private void OnDisable()
    {
        _eventSystem.OnResetButtonClick -= ResetVisual;
    }

    public void PressButton() => _animator.SetBool(IS_PRESSED, true);

    private void ResetVisual() => _animator.SetBool(IS_PRESSED, false);

}
