using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher instance;

    [SerializeField] private List<CinemachineVirtualCamera> cameras;



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


        EventDispatcher.RegisterFunction<int>("SwitchCamera", SwitchCamera);
    }


    void SwitchCamera(int cam_id)
    {
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.Priority = 0;
        }
        cameras[cam_id].Priority = 1;

    }
}
