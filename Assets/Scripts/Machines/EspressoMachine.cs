using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EspressoMachine : Machines, IInteractable
{


    public void Interact(Transform handle)
    {
        if (cupPlace.childCount > 0)
        {
            DoMyJob();
        }

        if (handle.childCount != 0)
        {
            product = handle.GetChild(0);
            if (product.tag == "Product")
            {
                if (cupPlace.childCount == 0)
                {
                    product.parent = cupPlace;
                    product.position = cupPlace.position;
                    product.GetComponent<Product>().work = false;
                    CameraSwitcher.instance.SwitchCamera(2);
                    ScriptsManager.instance.GoIce();
                }
                else
                {
                    Debug.Log("Espresso Machine is full");
                }
            }
            else
            {
                Debug.Log("Espresso Machine can only take coffee");
            }
        }


    }


    public void DoMyJob()
    {
        if (cupPlace.childCount >= 0)
        {
            product.GetComponent<Product>().work = true;
            //cupPlace.GetChild(0).GetComponent<Cup>().shot += 1;
            product.transform.gameObject.layer = 0;
            CameraSwitcher.instance.SwitchCamera(2);
            ScriptsManager.instance.GoFPS();
        }
    }



    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Canvas ShowMyUI()
    {
        myCanvas.gameObject.SetActive(true);
        return myCanvas;
    }



}
