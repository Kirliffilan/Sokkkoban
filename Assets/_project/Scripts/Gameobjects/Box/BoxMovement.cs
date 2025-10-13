using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    [SerializeField] private BoxVisual _boxVisual;
    [SerializeField] private ButtonVisual _requiredButton;

    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
    }

    public void Move(int dirx, int diry)
    {
        /////
        // if no floor under
        _boxVisual.Fall();
        /////
        ///// at the end of movement
        ///// courutine maybe
        if (transform.position == _requiredButton.transform.position)
            _requiredButton.PressButton();
    }

}
