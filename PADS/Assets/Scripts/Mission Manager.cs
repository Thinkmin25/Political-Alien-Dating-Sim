using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public bool missionSuccess;
    public float missionTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (missionTimer > 0)
        {
            missionTimer -= Time.deltaTime;
        }
    }

    public void MissionController(int missionSkillValue, int crewSkillValue, int missionLength)
    {
        missionSuccess = (crewSkillValue >= missionSkillValue);
        missionTimer = missionLength;
    }
}
