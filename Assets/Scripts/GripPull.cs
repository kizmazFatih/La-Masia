using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GripPull : MonoBehaviour
{
    [SerializeField] private GameObject grip;
    public Transform cup;

    private bool is_useable = true;
    private bool is_gripped = false;
    float zRotation;
    private float delta;
    private float first_y;
    [SerializeField] private float factor;



    private void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray ray1 = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray1, out RaycastHit hit, 100f))
        {
            if (hit.transform == transform.GetChild(2))
            {


                if (Input.GetMouseButtonDown(0) && is_useable)
                {
                    first_y = Input.mousePosition.y;
                    is_gripped = true;
                    zRotation = transform.eulerAngles.z;
                    if (zRotation > 180) zRotation -= 360;
                }


            }
            if (Input.GetMouseButton(0) && is_gripped)
            {
                delta = (Input.mousePosition.y - first_y) / factor;


                Vector3 rotation = new Vector3(0, -90, -delta + zRotation);
                rotation.z = Mathf.Clamp(rotation.z, -45, 30);


                transform.localRotation = Quaternion.Euler(rotation);
            }

        }
        if (Input.GetMouseButtonUp(0) && is_gripped)
        {
            is_gripped = false;
            is_useable = false;
            transform.DORotate(new Vector3(0, -90, -45), 1f).OnComplete(() => is_useable = true);
            if (transform.localEulerAngles.z > 29)
            {
                cup.GetComponent<Cup>().ice += 2;
            }
        }
    }



}
