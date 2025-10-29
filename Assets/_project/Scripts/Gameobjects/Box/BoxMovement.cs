using System.Collections;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    [SerializeField] private BoxVisual _boxVisual;
    [SerializeField] private ButtonLogic _requiredButton;
    [SerializeField] private float _tileSize = 1f;

    [Header("Floor Check")]
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private float _checkRadius = 0.5f;

    private BoxMovement[] _boxes;

    private EventSystem _eventSystem;

    private Vector3 _lastPosition;
    private bool _lastFallenState = false;
    private bool _lastCanMoveState = true;

    private Vector3 _startPosition;

    private bool _fallen = false;

    public bool Fallen => _fallen;

    public bool CanMove { get; private set; } = true;

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _startPosition = transform.position;
        _lastPosition = _startPosition;
    }

    private void Start()
    {
        _boxes = FindObjectsOfType<BoxMovement>();
    }

    private void OnEnable()
    {
        _eventSystem.OnPlayerMovement += SaveState;
        _eventSystem.OnUndoButtonClick += Return;
        _eventSystem.OnResetButtonClick += ResetPosition;
    }

    private void OnDisable()
    {
        _eventSystem.OnPlayerMovement -= SaveState;
        _eventSystem.OnUndoButtonClick -= Return;
        _eventSystem.OnResetButtonClick -= ResetPosition;
    }

    public bool CheckNextBox(int dirx, int diry)
    {
        Vector3 target = new(transform.position.x + dirx * _tileSize,
            transform.position.y + diry * _tileSize);
        foreach (var box in _boxes)
        {
            if (box == this || box.Fallen) continue;
            if (Vector3.Distance(box.transform.position, target) <= 0.01f)
            {
                return false;
            }
        }
        Move(dirx, diry);
        return true;
    }
    public void Move(int dirx, int diry)
    {
        if (!CanMove) return;

        Vector3 target = new(transform.position.x + dirx * _tileSize, 
            transform.position.y + diry * _tileSize);
        
        StartCoroutine(MoveCoroutine(target));

        if (Vector3.Distance(target, _requiredButton.transform.position) <= 0.01f)
        {
            CanMove = false;
            _requiredButton.PressButton();
        }
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, _tileSize * Time.deltaTime * 5);
            yield return null;
        }

        transform.position = targetPosition;

        CheckFloor(targetPosition);
    }

    private void CheckFloor(Vector2 targetPosition)
    {
        if(_fallen) return;

        Collider2D floor = Physics2D.OverlapCircle(targetPosition, _checkRadius, _floorLayer);

        if (floor == null)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        _fallen = true;
        _boxVisual.Fall();
        CanMove = false;
        yield return new WaitForSeconds(1f);
        CanMove = true;
    }

    private void SaveState()
    {
        _lastPosition = transform.position;
        _lastFallenState = _fallen;
        _lastCanMoveState = CanMove;
    }

    private void Return()
    {
        transform.position = _lastPosition;
        _fallen = _lastFallenState;
        CanMove = _lastCanMoveState;
        if (!_fallen)
            _boxVisual.ResetBox();
    }

    private void ResetPosition()
    {
        transform.position = _startPosition;
        CanMove = true;
        _fallen = false;
        _boxVisual.ResetBox();
        SaveState();
    }
}
