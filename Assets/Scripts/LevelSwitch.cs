using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LevelSwitch : MonoBehaviour
{
    public int currLevel = 0;

    public string[] levelNames = new string[4] { "Hub", "TomatoThrowing", "RoyalWardrobe", "TrainingDummy"};

    static LevelSwitch s = null;

    void Start()
    {
        if (s == null)
        {
            s = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void LevelSwitchHub()
    {
        currLevel = 0;
        SteamVR_LoadLevel.Begin(levelNames[currLevel]);
    }
    
    public void LevelSwitchTomato()
    {
        currLevel = 1;
        SteamVR_LoadLevel.Begin(levelNames[currLevel]);
    }

    public void LevelSwitchWardrobe()
    {
        currLevel = 2;
        SteamVR_LoadLevel.Begin(levelNames[currLevel]);
    }

    public void LevelSwitchDummy()
    {
        currLevel = 3;
        SteamVR_LoadLevel.Begin(levelNames[currLevel]);
    }
}
