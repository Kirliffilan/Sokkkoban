using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class ThemeApplier : MonoBehaviour
{
    private void Awake()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.color = PlayerPrefs.GetInt("Theme") switch
        {
            0 => Color.white,
            1 => Color.cyan,
            2 => Color.magenta,
            _ => Color.white,
        };
    }
}
