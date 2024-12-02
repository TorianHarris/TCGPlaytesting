using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Game/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    public Color32 Red;
    public Color32 Yellow;
    public Color32 Purple;
    public Color32 Green;
    public Color32 Blue;
    public Color32 White;
    public Color32 Gray;
    public Color32 Orange;
    public Color32 Black;
    public Color32 playerLogColor;
    public Color32 opponentLogColor;
}
