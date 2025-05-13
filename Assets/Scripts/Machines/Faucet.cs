using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : Machines, IInteractable
{
    [SerializeField] private TurnObjects turnObjects;
    private bool isOn = false;

    public void Interact(Transform handle)
    {


        if (isOn)
        {
            if (cupPlace.childCount > 0 && handle.childCount == 0)
            {
                DoMyJob();
                isOn = false;
            }
        }
        else
        {
            turnObjects.enabled = false;

            if (handle.childCount != 0 && cupPlace.childCount == 0)
            {
                CameraSwitcher.instance.SwitchCamera(0);
                ScriptsManager.instance.GoTurn();
                turnObjects.transform.GetComponent<Outline>().enabled = true;
                isOn = true;

                product = handle.GetChild(0);
                if (product.tag == "Product")
                {

                    product.parent = cupPlace;
                    product.position = cupPlace.position;
                    product.GetComponent<Product>().work = false;
                    turnObjects.cup = product;

                }

            }
            else if (cupPlace.childCount > 0 && handle.childCount == 0)
            {
                CameraSwitcher.instance.SwitchCamera(0);
                ScriptsManager.instance.GoTurn();
                turnObjects.transform.GetComponent<Outline>().enabled = true;
                isOn = true;
            }
        }
    }

    private void DoMyJob()
    {
        product.GetComponent<Product>().work = true;
        product.transform.gameObject.layer = 0;
        CameraSwitcher.instance.SwitchCamera(0);
        ScriptsManager.instance.GoFPS();
        turnObjects.GetComponent<Outline>().enabled = false;
    }

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Transform ShowMyUI()
    {
        myCanvas.gameObject.SetActive(true);
        return myCanvas.transform.parent;
    }




}
