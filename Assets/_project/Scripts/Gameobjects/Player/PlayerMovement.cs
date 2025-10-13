using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerVisual _playerVisual;

    private EventSystem _eventSystem;

    private BoxMovement[] _boxes;

    private Vector2 _lastPosition;

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _boxes = GetComponents<BoxMovement>();
    }

    private void OnEnable()
    {
        _eventSystem.OnResetButtonClick += ResetPosition;
        _eventSystem.OnUndoButtonClick += Return;
    }

    private void OnDisable()
    {
        _eventSystem.OnResetButtonClick -= ResetPosition;
        _eventSystem.OnUndoButtonClick -= Return;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // input

        int dirx = 0;/////
        int diry = 0;/////

        _lastPosition = transform.position;

        ///// maybe start courutine

        _playerVisual.Move(dirx, diry);

        Vector3 nextpos = Vector3.zero;/////

        foreach (var box in _boxes)
        {
            if (box.transform.position == nextpos)
            {
                box.Move(dirx, diry);
            }
        }

        _eventSystem.PlayerMove();
    }

    private void GetRandomPosition()
    {

    }

    private void ResetPosition()
    {
        GetRandomPosition();
    }

    private void Return()
    {
        transform.position = _lastPosition;
    }
}
