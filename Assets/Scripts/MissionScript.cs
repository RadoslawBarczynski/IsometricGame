using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionScript : MonoBehaviour
{
    public GameLogic gameLogic;
    public List<Mission> missionList = new List<Mission>();
    public TextMeshProUGUI Desc;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameLogic>();
        SetMissionText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckInteractablesAmount()
    {
        if (gameLogic.interactablesList.Count == 0)
        {
            CompleteMission(1);
        }
    }

    public void SetMissionText()
    {
        foreach(Mission mission in missionList)
        {
            Desc.text += mission.missionDesc + "\n";
        }
    }

    public void CompleteMission(int indexo)
    {
        Mission missionx = missionList[indexo];
        Desc.text = "";
        foreach (Mission mission in missionList)
        {
            if(mission == missionx)
            Desc.text +="<s> "+ mission.missionDesc + "</s> \n";
            else
                Desc.text += mission.missionDesc + "\n";
        }
    }
}
