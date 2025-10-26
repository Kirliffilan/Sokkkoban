using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private float _tileSize = 1f;
    [SerializeField] private Vector3[] _possibleStartPositions;

    [Header("Floor Check")]
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private float _checkRadius = 0.1f;

    private EventSystem _eventSystem;

    private BoxMovement[] _boxes;

    private Vector2 _lastPosition;

    private bool _canMove = true;
    private float _lastMoveTime;
    private readonly float _moveDelay = 0.2f;

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _boxes = FindObjectsOfType<BoxMovement>();
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
        if (!_canMove || Time.time - _lastMoveTime < _moveDelay)
            return;

        int dirx = 0;
        int diry = 0;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            diry = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            diry = -1;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dirx = -1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dirx = 1;
        else
        {
            _playerVisual.StopMovement();
            return;
        }


        Vector2 target = new(transform.position.x + dirx * _tileSize,
            transform.position.y + diry * _tileSize);

        _playerVisual.Move(dirx, diry);

        _eventSystem.PlayerMove();
        
        foreach (var box in _boxes)
        {
            if (Vector3.Distance(box.transform.position, target) < 0.01f)
            {
                if (box.CanMove)
                {
                    box.Move(dirx, diry);
                    break;
                }
                else
                {
                    return;
                }
            }
        }

        _lastPosition = transform.position;

        StartCoroutine(MoveCoroutine(target));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        _canMove = false;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, _tileSize * Time.deltaTime * 5);
            yield return null;
        }
        transform.position = targetPosition;
        _canMove = true;
        _lastMoveTime = Time.time;

        CheckFloor(targetPosition);
    }

    private void CheckFloor(Vector2 targetPosition)
    {
        Collider2D floor = Physics2D.OverlapCircle(targetPosition, _checkRadius, _floorLayer);

        if (floor == null)
        {
            _playerVisual.Fall();
            _canMove = false;
        }
    }

    private void GetRandomPosition()
    {
        transform.position = _possibleStartPositions[Random.Range(0, _possibleStartPositions.Length)];
    }

    private void ResetPosition()
    {
        StopAllCoroutines();
        GetRandomPosition();
        _canMove = true;
        _playerVisual.ResetPlayer();
    }

    private void Return()
    {
        StopAllCoroutines();
        _canMove = true;
        _playerVisual.ResetPlayer();
        transform.position = _lastPosition;
    }
}
