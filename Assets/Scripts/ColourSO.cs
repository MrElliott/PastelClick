using UnityEngine;

[CreateAssetMenu(fileName = "Colour SO", menuName = "ScriptableObjects/ColourObject", order=1)]
public class ColourSO : ScriptableObject
{
    public Color BackgroundColour;
    public Color[] ClickColours;
}
