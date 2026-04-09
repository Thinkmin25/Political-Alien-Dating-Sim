using UnityEngine;
using UnityEngine.UI;

public class RelationshipBar : MonoBehaviour
{
    public Image barImage;
    public static int targetValue = 0;
    float currentValue = 0;
    public float barVel = 0;
    public float barAccel;
    public float barStopRange;
    bool slowDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RelationshipManager.politiciaDict["relationValue"]--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RelationshipManager.politiciaDict["relationValue"]++;
        }

        targetValue = RelationshipManager.politiciaDict["relationValue"];
        if (currentValue != targetValue)
        {
            if (currentValue < targetValue)
            {
                if (slowDown)
                {
                    barVel += barAccel * Time.deltaTime / 2;
                }
                else
                {
                    barVel += barAccel * Time.deltaTime;
                }
            }
            else if (currentValue > targetValue)
            {
                if (slowDown)
                {
                    barVel -= barAccel * Time.deltaTime / 2;
                }
                else
                {
                    barVel -= barAccel * Time.deltaTime;
                }
            }
            currentValue += barVel * Time.deltaTime;

            if (!slowDown && Mathf.Abs(currentValue - targetValue) < barStopRange * 1.5f)
            {
                slowDown = true;
            }

            if (Mathf.Abs(currentValue - targetValue) < barStopRange)
            {
                slowDown = false;
                barVel = 0;
                currentValue = targetValue;
            }

            barImage.rectTransform.sizeDelta = new Vector2(currentValue * 325 / 15 + 75, 65);
        }
    }

    public static void GetBarNum(int relationShipValue)
    {
        targetValue = relationShipValue;
    }
}
