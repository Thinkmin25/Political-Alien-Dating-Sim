using UnityEngine;

public class MenuTracker : MonoBehaviour
{
    string screenName = "";

    int[] screenVisitCount = new int[4];
    float[] screenTimeSpent = new float[4];


    // Update is called once per frame
    void Update()
    {
        switch (screenName)
        {
            case "Crewmate Screen":
                screenTimeSpent[0] += Time.time;
                break;
            case "Navigation Screen":
                screenTimeSpent[1] += Time.time;
                break;
            case "Ship Screen":
                screenTimeSpent[2] += Time.time;
                break;
            case "Dialogue Screen":
                screenTimeSpent[3] += Time.time;
                break;
        }
    }

    public void EnterScreen(string screen)
    {
        switch (screen)
        {
            case "Crewmate Screen":
                screenVisitCount[0]++;
                break;
            case "Navigation Screen":
                screenVisitCount[1]++;
                break;
            case "Ship Screen":
                screenVisitCount[2]++;
                break;
            case "Dialogue Screen":
                screenVisitCount[3]++;
                break;
        }

        screenName = screen;
    }
}
