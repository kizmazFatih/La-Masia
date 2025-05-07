using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMachine : Machines, IInteractable
{

    [SerializeField] private GripPull gripPull;
    private bool isOn = false;

    public void Interact(Transform handle)
    {
        if (isOn)
        {
            if (cupPlace.childCount > 0 && handle.childCount == 0)
            {
                DoMyJob();
            }
        }
        else
        {
            CameraSwitcher.instance.SwitchCamera(1);
            ScriptsManager.instance.GoIce();

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

                        gripPull.cup = product;
                    }
                }
            }
        }
        isOn = !isOn;
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
            product.GetComponent<Product>().work = true;
            product.transform.gameObject.layer = 0;
            CameraSwitcher.instance.SwitchCamera(1);
            ScriptsManager.instance.GoFPS();
        }
    }


}


