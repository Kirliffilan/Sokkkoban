using UnityEngine;

public class UndoButton : MonoBehaviour
{
    private EventSystem _eventSystem;

    private int _undoCount = 1;

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        _eventSystem.OnResetButtonClick += ResetUndoCount;
    }

    private void OnDisable()
    {
        _eventSystem.OnResetButtonClick -= ResetUndoCount;
    }

    public void TryUndoMovement()
    {
        if (_undoCount > 0)
            _eventSystem.UndoMovement();
    }

    private void ResetUndoCount()
    {
        _undoCount = 1;
    }
}
