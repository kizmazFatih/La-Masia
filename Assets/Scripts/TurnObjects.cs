using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnObjects : MonoBehaviour
{
    [SerializeField] private RawImage cursor;
    public float sensitivity = 2.0f;
    private Vector2 first_position;
    private Vector2 last_position;

    private float angle;
    private float default_rotation;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            first_position = Input.mousePosition;
            default_rotation = transform.localEulerAngles.z;
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log(default_rotation);
            last_position = Input.mousePosition;
            angle = Mathf.Atan2(last_position.y - first_position.y, last_position.x - first_position.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, transform.eulerAngles.y, default_rotation + angle);
            
        }


    }
}
