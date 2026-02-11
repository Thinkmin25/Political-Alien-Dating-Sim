using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Scriptable Objects/PlayerInventory")]
public static class PlayerInventory
{
    public static int credits = 50;
    public static int fuel = 100;

    public static Dictionary<string, int> giftDictionary = new Dictionary<string, int>();
    public static Dictionary<string, int> matDictionary = new Dictionary<string, int>();

    public static void Start()
    {
        giftDictionary.Add("gift1", 0);
        giftDictionary.Add("gift2", 0);
        giftDictionary.Add("gift3", 0);
        giftDictionary.Add("gift4", 0);

        matDictionary.Add("mat1", 0);
        matDictionary.Add("mat2", 0);
        matDictionary.Add("mat3", 0);
        matDictionary.Add("mat4", 0);
    }
}
