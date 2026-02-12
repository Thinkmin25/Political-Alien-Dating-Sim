using UnityEngine;
using UnityEngine.UI;

public class RelationshipBar : MonoBehaviour
{
    public Image barImage;
    public static int targetValue = 0;
    float currentValue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        targetValue = RelationshipManager.politiciaDict["relationValue"];
        if (currentValue != targetValue)
        {
            currentValue = targetValue;
            barImage.rectTransform.sizeDelta = new Vector2(currentValue * 325 / 15 + 75, 65);
        }
    }

    public static void GetBarNum(int relationShipValue)
    {
        targetValue = relationShipValue;
    }
}
