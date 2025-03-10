using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    public static ScriptsManager instance;

    [SerializeField] private FPSController fpsController;
    [SerializeField] private TurnObjects turnObjects;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


    public void GoFPS()
    {
        fpsController.enabled = true;
        turnObjects.enabled = false;
    }

    public void GoTurn()
    {
        fpsController.enabled = false;
        turnObjects.enabled = true;
    }

}
