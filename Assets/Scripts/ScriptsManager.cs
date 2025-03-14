using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    public static ScriptsManager instance;

    [SerializeField] private FPSController fpsController;
    [SerializeField] private TurnObjects turnObjects;
    [SerializeField] private GripPull gripPull;

    private List<ScriptReference> scripts = new List<ScriptReference>();


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
        gripPull.enabled = false;
    }

    public void GoTurn()
    {
        fpsController.enabled = false;
        turnObjects.enabled = true;
    }
    public void GoIce()
    {
        fpsController.enabled = false;
        gripPull.enabled = true;
    }

}
