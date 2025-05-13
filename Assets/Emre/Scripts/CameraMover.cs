using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{
    
    public Transform[] cameraPoints;
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;

    private bool isMoving = false;
    public GameObject menuCanvas;

    void Start()
    {
        transform.position = cameraPoints[0].position;
        transform.rotation = cameraPoints[0].rotation;
    }

    public void MoveToPoint(int pointIndex)
    {
        if (!isMoving)
            StartCoroutine(MoveCamera(cameraPoints[pointIndex]));
    }
    IEnumerator MoveCamera(Transform target)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;
        isMoving = false;
    }
    public void MoveAlongPath(Transform[] pathPoints)
    {
        if (!isMoving)
            StartCoroutine(MoveCameraPath(pathPoints));
    }

    IEnumerator MoveCameraPath(Transform[] path)
    {
        isMoving = true;

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;

            Vector3 endPos = path[i].position;
            Quaternion endRot = path[i].rotation;

            float journey = 0f;
            float duration = Vector3.Distance(startPos, endPos) / moveSpeed * 0.1f;

            while (journey < duration)
            {
                journey += Time.deltaTime;
                float t = journey / duration;
                t = t * t * (3f - 2f * t); // SmoothStep easing

                transform.position = Vector3.Lerp(startPos, endPos, t);
                transform.rotation = Quaternion.Slerp(startRot, endRot, t);

                yield return null;
            }

            transform.position = endPos;
            transform.rotation = endRot;
        }

        isMoving = false;

        // Son hedef Tabela ise Canvas'ı aç
        if (path.Length > 0 && path[^1] == cameraPoints[1])
        {
            if (menuCanvas != null)
                menuCanvas.SetActive(true);
        }
    }


}