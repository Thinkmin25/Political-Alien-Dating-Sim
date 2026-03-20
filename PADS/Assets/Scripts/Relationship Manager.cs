using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour
{
    public static Dictionary<string, int> politiciaDict = new Dictionary<string, int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        politiciaDict.Add("relationValue", 5);
    }
}
