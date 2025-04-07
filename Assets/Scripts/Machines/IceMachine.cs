using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMachine : Machines, IInteractable
{

    [SerializeField] private GripPull gripPull;
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
                    CameraSwitcher.instance.SwitchCamera(1);
                    ScriptsManager.instance.GoIce();
                    gripPull.cup = product;
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

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Canvas ShowMyUI()
    {
        myCanvas.gameObject.SetActive(true);
        return myCanvas;
    }

    public void DoMyJob()
    {
        if (cupPlace.childCount >= 0)
        {
            product.GetComponent<Cup>().ice += 2;
            product.GetComponent<Product>().work = true;
            product.transform.gameObject.layer = 0;
            CameraSwitcher.instance.SwitchCamera(1);
            ScriptsManager.instance.GoFPS();

        }
    }


}


