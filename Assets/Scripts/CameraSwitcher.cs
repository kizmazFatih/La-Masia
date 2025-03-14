using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher instance;
    public List<CameraSO> cameraSO;
    private bool bounce = false;

    [SerializeField] private CinemachineVirtualCamera fps_camera;
    [SerializeField] private CinemachineVirtualCamera machine_camera;



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


    public void SwitchCamera(int cam_id)
    {
        /* foreach (CinemachineVirtualCamera camera in cameras)
         {
             camera.Priority = 0;
         }
         cameras[cam_id].Priority = 1;*/
        cameraSO[cam_id].ApplySettings(machine_camera);
        bounce = !bounce;
        machine_camera.Priority = bounce ? 2 : 0;
    }





}
