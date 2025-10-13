using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event Action OnResetButtonClick;
    public event Action OnUndoButtonClick;
    public event Action OnPlayerMovement;

    public void PlayerMove() => OnPlayerMovement?.Invoke();
    public void UndoMovement() => OnUndoButtonClick?.Invoke();
    public void ResetLevel() => OnResetButtonClick?.Invoke();
}
