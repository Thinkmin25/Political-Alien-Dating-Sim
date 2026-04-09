using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrewmateManager : MonoBehaviour
{
    public static List<Crewmate> crewmates = new List<Crewmate>();
    public string[] crewmateNames;
    public string[] crewmateDescriptions;
    public Sprite[] crewmatePortraitSprites;
    public Sprite[] crewmateIconSprites;
    public int[] crewmateBody;
    public int[] crewmateMind;
    public int[] crewmateSoul;

    public GameObject currentCrewOne;
    public GameObject currentCrewTwo;

    public static int crewOneIndex = 0;
    public static int crewTwoIndex = 0;

    int startingCrewmateCount = 3;

    public TMP_Text nameText;
   // public TMP_Text descriptionText;
    public GameObject[] crewIconArray;
    //public Image portraitImage;

    

    public Slider[] statBars;
    float[] startStats = new float[3] {0, 0, 0};
    public static float[] endStats = new float[3] {0, 0, 0};
    float timer = 0;
    float timerSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //for (int i = 0; i < startingCrewmateCount; i++)
        //{
        //    CreateCrewmate(i);
        //}

        for (int i = 0; i < crewIconArray.Length; i++)
        {
            var iconRef = crewIconArray[i].GetComponent<CrewmateSelector>();
            iconRef.crewOneGO = currentCrewOne;
            iconRef.crewTwoGO = currentCrewTwo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer <= 1)
        {
            timer += Time.deltaTime * timerSpeed;
            for (int i = 0; i < 3; i++)
            {
                statBars[i].value = Mathf.Lerp(startStats[i], endStats[i] / 10, Mathf.Sin((Mathf.PI * timer) / 2));
            }
        }
    }

    public void StatChange(int index)
    {
        nameText.text = crewmateNames[index];


        timer = 0;

        startStats[0] = statBars[0].value;
        startStats[1] = statBars[1].value;
        startStats[2] = statBars[2].value;

        if (crewOneIndex == index)
        {
            endStats[0] -= crewmateBody[crewOneIndex];
            endStats[1] -= crewmateMind[crewOneIndex];
            endStats[2] -= crewmateSoul[crewOneIndex];
            crewOneIndex = 0;

        }
        else
        if (crewTwoIndex == index)
        {
            endStats[0] -= crewmateBody[crewTwoIndex];
            endStats[1] -= crewmateMind[crewTwoIndex];
            endStats[2] -= crewmateSoul[crewTwoIndex];
            crewTwoIndex = 0;
        }
        else
        {
            endStats[0] += crewmateBody[index] - crewmateBody[crewOneIndex];
            endStats[1] += crewmateMind[index] - crewmateMind[crewOneIndex];
            endStats[2] += crewmateSoul[index] - crewmateSoul[crewOneIndex];
            crewOneIndex = crewTwoIndex;
            crewTwoIndex = index;
        }

        // this is so gross im gonna change it so soon
        Color crewOneColor = Color.white;
        if (crewOneIndex == 0)
        {
            crewOneColor = Color.white;
        }
        else if (crewOneIndex == 1)
        {
            crewOneColor = new Color(1, 0.66f, 0.33f);
        }
        else if (crewOneIndex == 2)
        {
            crewOneColor = Color.green;
        }
        else if (crewOneIndex == 3)
        {
            crewOneColor = Color.cyan;
        }
        currentCrewOne.GetComponent<Image>().color = crewOneColor;

        Color crewTwoColor = Color.white;
        if (crewTwoIndex == 0)
        {
            crewTwoColor = Color.white;
        }
        else if (crewTwoIndex == 1)
        {
            crewTwoColor = new Color(1, 0.66f, 0.33f);
        }
        else if (crewTwoIndex == 2)
        {
            crewTwoColor = Color.green;
        }
        else if (crewTwoIndex == 3)
        {
            crewTwoColor = Color.cyan;
        }
        currentCrewTwo.GetComponent<Image>().color = crewTwoColor;
    }

    public static void ChangeCrew(int crewNum)
    {
        if (crewNum == 1)
        {

        }
    }

    //public void CreateCrewmate(int index)
    //{
    //    Crewmate currentCrewmate = new Crewmate();
    //    currentCrewmate.crewName = crewmateNames[index];
    //    currentCrewmate.crewDescription = crewmateDescriptions[index];
    //    currentCrewmate.crewBodySprite = crewmatePortraitSprites[index];
    //    currentCrewmate.crewIconSprite = crewmateIconSprites[index];
    //    currentCrewmate.bodyStat = crewmateBody[index];
    //    currentCrewmate.mindStat = crewmateMind[index];
    //    currentCrewmate.soulStat = crewmateSoul[index];

    //    crewmates.Add(currentCrewmate);
    //}
}
