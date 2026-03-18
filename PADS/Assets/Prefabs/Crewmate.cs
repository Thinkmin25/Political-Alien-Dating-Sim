using System.Collections.Generic;
using UnityEngine;

public class Crewmate : MonoBehaviour
{
    public string crewName;
    public string crewDescription;
    public Sprite crewBodySprite;
    public Sprite crewIconSprite;

    public Dictionary<string, int> skillDict; // = new Dictionary<string, int>() {{"a", 2}, { "b", 4 }, { "c", 5 }}

    public int bodyStat;
    public int mindStat;
    public int soulStat;
}
