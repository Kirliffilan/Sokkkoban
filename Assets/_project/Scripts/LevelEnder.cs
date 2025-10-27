using System.Collections;
using UnityEngine;

public class LevelEnder : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _winMenu;
    
    private ButtonLogic[] buttons;
    private EventSystem _eventSystem;

    private void Awake()
    {
        buttons = FindObjectsOfType<ButtonLogic>();
        _eventSystem = FindAnyObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        _eventSystem.OnButtonPressed += CheckLevelEnd;
    }

    private void OnDisable()
    {
        _eventSystem.OnButtonPressed -= CheckLevelEnd;
    }

    private void CheckLevelEnd()
    {
        foreach (var button in buttons)
        {
            if (!button.IsPressed)
                return;
        }
        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(0.5f);
        _winMenu.gameObject.SetActive(true);
    }
}
