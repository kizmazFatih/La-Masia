using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class RaySystem : MonoBehaviour
{
    [SerializeField] private Transform handle;


    private float rayDistance = 5f;
    private Canvas canvas = null;
    private bool canvasActive = false;
    private IInteractable lastInteractable = null;





    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && handle.childCount != 0)
        {
            handle.GetChild(0).GetComponent<IInteractable>().Release(handle);
        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, rayDistance))
        {

            if (handle.childCount != 0 && handle.GetChild(0).tag != "Pistol") //Elimizde pistol varsa ışını 15 metre olarak ayarlıyoruz ve diğer etkileşimleri blokluyor
            {
               rayDistance = 15f;

                if (Input.GetMouseButtonDown(0))
                {
                    //Shoot(hit);
                    EventDispatcher.SummonEvent("Shoot", hit);
                }
            }
            else
            {
                var currentInteractable = hit.transform.GetComponent<IInteractable>();
                rayDistance = 5f;

                if (currentInteractable != null)
                {
                    if (lastInteractable != null && currentInteractable != lastInteractable)//Işın farklı bir objeye değerse önceki objenin UI'sini kapatıyoruz
                    {
                        CloseCanvas();
                    }
                    canvas = currentInteractable.ShowMyUI();
                    canvasActive = true;
                    lastInteractable = currentInteractable;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        currentInteractable.Interact(handle);
                    }

                }
                else
                {
                    CloseCanvas();
                }



                if (hit.transform.gameObject.GetComponent<ILeftClick>() != null)
                {
                    if (Input.GetMouseButton(0))
                    {
                        hit.transform.gameObject.GetComponent<ILeftClick>().DoMyJob(handle);
                    }
                }
            }



        }
        else CloseCanvas();


        if (canvas != null && canvasActive)
        {
            canvas.transform.LookAt(Camera.main.transform);
        }

    }



    void CloseCanvas()
    {
        if (canvas != null && canvasActive)
        {
            canvas.gameObject.SetActive(false);
            canvasActive = false;
        }
    }


    /*void Shoot(RaycastHit hit)
    {
        if (hit.transform.tag == "Customer")
        {
            GameObject hit_object = hit.transform.root.gameObject;
            Rigidbody[] rigidbodies = hit_object.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }

            hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * 15f, hit.point, ForceMode.Impulse);
        }

    }*/

}
