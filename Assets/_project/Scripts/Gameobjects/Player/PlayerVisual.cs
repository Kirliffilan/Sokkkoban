using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private readonly int X = Animator.StringToHash("X");
    private readonly int Y = Animator.StringToHash("Y");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(int dirx, int diry)
    {
        _animator.SetFloat(X, dirx);
        _animator.SetFloat(Y, diry);
    }

    public void StopMovement()
    {
        _animator.SetFloat(X, 0);
        _animator.SetFloat(Y, 0);
    }
}
