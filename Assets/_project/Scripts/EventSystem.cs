using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event Action OnResetButtonClick;
    public event Action OnUndoButtonClick;
    public event Action OnPlayerMovement;
    public event Action OnButtonPressed;

    public void PlayerMove() => OnPlayerMovement?.Invoke();
    public void UndoMovement() => OnUndoButtonClick?.Invoke();
    public void ResetLevel() => OnResetButtonClick?.Invoke();
    public void ButtonPressed() => OnButtonPressed?.Invoke();
}
