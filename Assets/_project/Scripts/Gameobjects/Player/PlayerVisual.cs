using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private readonly int X = Animator.StringToHash("X");
    private readonly int Y = Animator.StringToHash("Y");
    private readonly int FALL = Animator.StringToHash("Fall");
    private readonly int RESET = Animator.StringToHash("Reset");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(int dirx, int diry)
    {
        _animator.SetInteger(X, dirx);
        _animator.SetInteger(Y, diry);
    }

    public void StopMovement()
    {
        _animator.SetInteger(X, 0);
        _animator.SetInteger(Y, 0);
    }

    public void Fall()
    {
        _animator.SetTrigger(FALL);
    }

    public void ResetPlayer()
    {
        _animator.SetTrigger(RESET);
    }
}
