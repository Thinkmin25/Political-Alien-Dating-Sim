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

    public int crewOne;
    public int crewTwo;

    int startingCrewmateCount = 3;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image[] iconImages;
    public Image portraitImage;

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
        timer = 0;

        startStats[0] = statBars[0].value;
        startStats[1] = statBars[1].value;
        startStats[2] = statBars[2].value;

        if (crewOne == index)
        {
            endStats[0] -= crewmateBody[crewOne];
            endStats[1] -= crewmateMind[crewOne];
            endStats[2] -= crewmateSoul[crewOne];
            crewOne = 0;

        }
        else
        if (crewTwo == index)
        {
            endStats[0] -= crewmateBody[crewTwo];
            endStats[1] -= crewmateMind[crewTwo];
            endStats[2] -= crewmateSoul[crewTwo];
            crewTwo = 0;
        }
        else
        {
            endStats[0] += crewmateBody[index] - crewmateBody[crewOne];
            endStats[1] += crewmateMind[index] - crewmateMind[crewOne];
            endStats[2] += crewmateSoul[index] - crewmateSoul[crewOne];
            crewOne = crewTwo;
            crewTwo = index;
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
