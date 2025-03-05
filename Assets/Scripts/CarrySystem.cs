using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrySystem : MonoBehaviour
{
    private GameObject hold_object;
    private bool hand_full = false;

    [SerializeField] private Transform handle;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && hand_full == true)
        {
            ReleaseObject();
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f))
        {

            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);

            if (hit.transform.gameObject.tag == "Product" && Input.GetKeyDown(KeyCode.E))
            {

                if (hand_full == false)
                {
                    hold_object = hit.transform.gameObject;
                    TakeObject();
                }
            }
        }




    }

    void TakeObject()
    {
        hold_object.GetComponent<Rigidbody>().isKinematic = true;
        hold_object.transform.parent = handle;
        hold_object.transform.localPosition = Vector3.zero;
        hold_object.transform.localRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(SetHandState());
    }

    void ReleaseObject()
    {
        hold_object.GetComponent<Rigidbody>().isKinematic = false;
        hold_object.transform.parent = null;
        StartCoroutine(SetHandState());
    }

    IEnumerator SetHandState()
    {
        yield return new WaitForSeconds(0.5f);
        hand_full = !hand_full;
        Debug.Log(hand_full);
    }

}
