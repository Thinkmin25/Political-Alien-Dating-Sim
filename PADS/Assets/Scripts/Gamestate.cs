using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public static int gameProgressionIndex = 0;
    public static string dialogueKey = "Tutorial";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameProgressionIndex)
        {
            case 0:
                dialogueKey = "Tutorial";
                break;
            case 1:
                dialogueKey = "Exercise";
                break;
            case 2:
                dialogueKey = "Intro";
                break;
            case -1:
                dialogueKey = "Wrong Exercise";
                    break;
        }
    }

    public void CheckCrew(CrewmateManager cmScript)
    {
        var crew1 = cmScript.crewOne;
        var crew2 = cmScript.crewTwo;

        if (crew1 != crew2 && (crew1 == 1 || crew1 == 2) && (crew2 == 1 || crew2 == 2))
        {
            gameProgressionIndex = 1;
        }
        else
        {
            gameProgressionIndex = -1;
        }

        switch (gameProgressionIndex)
        {
            case 0:
                dialogueKey = "Tutorial";
                break;
            case 1:
                dialogueKey = "Exercise";
                break;
            case 2:
                dialogueKey = "Intro";
                break;
            case -1:
                dialogueKey = "Wrong Exercise";
                break;
        }
    }
}
