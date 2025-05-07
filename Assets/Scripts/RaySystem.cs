using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySystem : MonoBehaviour
{
    [SerializeField] private Transform handle;


    private float rayDistance = 5f;
    private Canvas canvas = null;
    private bool canvasActive = false;
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


                //Open Outline
                var outline = hit.transform.GetComponent<Outline>();
                if (outline != null) outline.enabled = true;
                //--------------------------------------------------

                if (currentInteractable != null)
                {
                    if (lastInteractable != null && currentInteractable != lastInteractable)//Işın farklı bir objeye değerse önceki objenin UI'sini kapatıyoruz
                    {
                        CloseCanvasAndOutline();
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
                    CloseCanvasAndOutline();
                }



                /*if (hit.transform.gameObject.GetComponent<ILeftClick>() != null)
                {
                    if (Input.GetMouseButton(0))
                    {
                        hit.transform.gameObject.GetComponent<ILeftClick>().DoMyJob(handle);
                    }
                }*/
            }



        }
        else CloseCanvasAndOutline();


        if (canvas != null && canvasActive)
        {
            canvas.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }

    }



    void CloseCanvasAndOutline()
    {
        if (canvas != null && canvasActive)
        {
            canvas.gameObject.SetActive(false);
            canvasActive = false;
            canvas.transform.parent.GetComponent<Outline>().enabled = false;
        }
    }


}
