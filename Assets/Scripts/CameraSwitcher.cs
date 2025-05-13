using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher instance;
    public List<CameraSO> cameraSO;
    private bool bounce = false;

    [SerializeField] private RaySystem raySystem;
    [SerializeField] private CinemachineVirtualCamera fps_camera;
    [SerializeField] private CinemachineVirtualCamera machine_camera;
    int characterLayer;
    int armsLayer;





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
        characterLayer = LayerMask.NameToLayer("Character");
        armsLayer = LayerMask.NameToLayer("Arms");
    }


    public void SwitchCamera(int cam_id)
    {
        cameraSO[cam_id].ApplySettings(machine_camera);
        bounce = !bounce;
        machine_camera.Priority = bounce ? 2 : 0;
    }



    public void MachineCameraEvent()
    {
        Camera.main.cullingMask &= ~(1 << characterLayer);
        Camera.main.cullingMask &= ~(1 << armsLayer);
        raySystem.machineCameraActive = true;

        Cursor.visible = true; // İmleci görünür yapar
        Cursor.lockState = CursorLockMode.None; // İmlecin serbestçe hareket etmesini sağlar
    }

    public void FPSCameraEvent()
    {
        Camera.main.cullingMask &= ~(1 << characterLayer);
        Camera.main.cullingMask |= 1 << armsLayer;
        raySystem.machineCameraActive = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}


