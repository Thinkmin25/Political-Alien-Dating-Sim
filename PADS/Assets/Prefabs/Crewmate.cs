using UnityEngine;

[CreateAssetMenu(fileName = "Crewmate", menuName = "Scriptable Objects/Crewmate")]
public class Crewmate : ScriptableObject
{
    public string crewName;
    public string crewDescription;
    public Sprite crewBodySprite;
    public Sprite crewIconSprite;

    public int stat1;
    public int stat2;
    public int stat3;
    public int stat4;
}
