using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoxVisual : MonoBehaviour
{
    private EventSystem _eventSystem;
    
    private Animator _animator;

    private readonly int FALL = Animator.StringToHash("Fall");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _eventSystem = FindAnyObjectByType<EventSystem>();
    }

    public void Fall()
    {
        _animator.SetTrigger(FALL);
    }

    private void ResetBox()
    {
        gameObject.SetActive(true);
    }
}
