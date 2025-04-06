using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : Machines, IInteractable
{
    [SerializeField] private TurnObjects turnObjects;

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
                    CameraSwitcher.instance.SwitchCamera(0);
                    ScriptsManager.instance.GoTurn();
                    turnObjects.cup = product;
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
        product.GetComponent<Product>().work = true;
        CameraSwitcher.instance.SwitchCamera(0);
        ScriptsManager.instance.GoFPS();
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
