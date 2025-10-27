using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ThemePicker : MonoBehaviour
{
    [SerializeField] private int _pickedTheme;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        image.color = _pickedTheme switch
        {
            0 => Color.white,
            1 => Color.cyan,
            2 => Color.magenta,
            _ => Color.white,
        };
    }

    public void PickTheme()
    {
        PlayerPrefs.SetInt("Theme", _pickedTheme);
        PlayerPrefs.Save();
    }
}
