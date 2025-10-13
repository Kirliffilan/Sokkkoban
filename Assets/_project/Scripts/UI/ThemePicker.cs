using UnityEngine;

public class ThemePicker : MonoBehaviour
{
    private int _pickedTheme;

    private void Awake()
    {
        _pickedTheme = PlayerPrefs.GetInt("Theme");   
        /////
    }

    private void PickTheme(int pickedTheme)
    {
        _pickedTheme = pickedTheme;
        PlayerPrefs.SetInt("Theme", pickedTheme);
    }
}
