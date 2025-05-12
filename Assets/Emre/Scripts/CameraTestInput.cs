using UnityEngine;

public class CameraTestInput : MonoBehaviour
{
    private CameraMover camMover;
    public Transform pathPoint1;

    void Start()
    {
        camMover = GetComponent<CameraMover>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Başlangıç → Path1 → Tabela
            Transform[] path = new Transform[]
            {
                pathPoint1,
                camMover.cameraPoints[1] // CamPoint_Tabela
            };
            camMover.MoveAlongPath(path);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            camMover.MoveToPoint(1); // Tabela
        if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Transform[] path = new Transform[]
                {
                    pathPoint1,
                    camMover.cameraPoints[2] // CamPoint_Settings
                };
                camMover.MoveAlongPath(path);
            }
        if (Input.GetKeyDown(KeyCode.Alpha4))
            camMover.MoveToPoint(3); // Exit
    }
}