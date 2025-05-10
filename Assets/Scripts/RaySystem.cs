using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySystem : MonoBehaviour
{
    [SerializeField] private Transform handle;


    private float rayDistance = 5f;
    private Transform activeObject = null;
    private GameObject canvas;
    private bool canvasActive = false;
    public bool machineCameraActive = false;
    private IInteractable lastInteractable = null;
    [SerializeField] private LayerMask ignoreLayer;





    void Update()
    {
        if (handle.childCount != 0)
        {
            handle.GetChild(0).transform.gameObject.layer = 2;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                handle.GetChild(0).transform.gameObject.layer = 0;
                handle.GetChild(0).GetComponent<IInteractable>().Release(handle);
            }
        }


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, rayDistance, ~ignoreLayer))
        {

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayDistance, Color.red);
            if (handle.childCount != 0 && handle.GetChild(0).tag == "Pistol") //Elimizde pistol varsa ışını 15 metre olarak ayarlıyoruz ve diğer etkileşimleri blokluyor
            {
                rayDistance = 15f;

                CloseCanvasAndOutline();
                if (Input.GetMouseButtonDown(0))
                {
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
                        CloseCanvasAndOutline();
                    }
                    activeObject = currentInteractable.ShowMyUI();
                    //activeObject.GetComponent<Outline>().enabled = true;
                    if (!machineCameraActive) { activeObject.GetComponent<Outline>().enabled = true; }
                    else { activeObject.GetComponent<Outline>().enabled = false; }

                    canvasActive = true;
                    lastInteractable = currentInteractable;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        currentInteractable.Interact(handle);
                    }

                }
                else
                {
                    CloseCanvasAndOutline();
                }
            }



        }
        else CloseCanvasAndOutline();


        if (activeObject != null && canvasActive)
        {
            if (activeObject.childCount != 0)
            {
                canvas = activeObject.GetChild(0).gameObject;
                canvas.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }
        }

    }



    public void CloseCanvasAndOutline()
    {
        if (activeObject != null && canvasActive)
        {
            canvas?.SetActive(false);
            canvasActive = false;
            activeObject.transform.GetComponent<Outline>().enabled = false;
        }
    }


}
