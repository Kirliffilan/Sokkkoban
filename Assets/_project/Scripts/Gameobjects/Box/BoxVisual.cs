using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoxVisual : MonoBehaviour
{
    private Animator _animator;

    private readonly int FALL = Animator.StringToHash("Fall");
    private readonly int RESET = Animator.StringToHash("Reset");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Fall()
    {
        _animator.SetTrigger(FALL);
    }

    public void ResetBox()
    {
        _animator.SetTrigger(RESET);
    }
}
