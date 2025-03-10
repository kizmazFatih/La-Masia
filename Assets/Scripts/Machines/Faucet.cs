using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : Machines, IInteractable
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
                    EventDispatcher.SummonEvent("SwitchCamera");
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

    private void DoMyJob()
    {
        if (cupPlace.childCount >= 0)
        {
            cupPlace.GetChild(0).GetComponent<Cup>().water += 1;
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
