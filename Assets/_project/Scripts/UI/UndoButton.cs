using UnityEngine;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private int _undoCount = 1;

    private EventSystem _eventSystem;

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
        {
            _eventSystem.UndoMovement();
            _undoCount--;
        }
    }

    private void ResetUndoCount()
    {
        _undoCount = 1;
    }
}
