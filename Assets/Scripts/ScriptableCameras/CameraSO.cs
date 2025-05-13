using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/CameraSO")]

public class CameraSO : ScriptableObject
{

    public Vector3 camera_position;
    public Vector3 camera_rotation;
    public Transform follow;
    public Transform lookAt;
    public float min_horizontal, max_horizontal;
    public LayerMask ignore_layer;


    public void ApplySettings(CinemachineVirtualCamera camera)
    {
        camera.Follow = follow;
        camera.LookAt = lookAt;
        camera.transform.position = camera_position;
        camera.transform.rotation = Quaternion.Euler(camera_rotation);
        camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxValue = max_horizontal;
        camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MinValue = min_horizontal;
        Camera.main.cullingMask = ~ignore_layer;
    }

}
