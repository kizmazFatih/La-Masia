using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMachine : Machines, IInteractable
{

    [SerializeField] private GripPull gripPull;
    public bool isOn = false;

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


            if (handle.childCount != 0 && cupPlace.childCount == 0)
            {
                CameraSwitcher.instance.SwitchCamera(1);
                ScriptsManager.instance.GoIce();
                gripPull.transform.GetComponent<Outline>().enabled = true;
                isOn = true;

                product = handle.GetChild(0);
                if (product.tag == "Product")
                {

                    product.parent = cupPlace;
                    product.position = cupPlace.position;
                    product.GetComponent<Product>().work = false;

                    gripPull.cup = product;

                }
            }
            else if (cupPlace.childCount > 0 && handle.childCount == 0)
            {
                CameraSwitcher.instance.SwitchCamera(1);
                ScriptsManager.instance.GoIce();
                gripPull.transform.GetComponent<Outline>().enabled = true;
                isOn = true;
            }
        }

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

    public void DoMyJob()
    {
        if (cupPlace.childCount >= 0)
        {
            product.GetComponent<Product>().work = true;
            product.transform.gameObject.layer = 0;
            CameraSwitcher.instance.SwitchCamera(1);
            ScriptsManager.instance.GoFPS();
            gripPull.transform.GetComponent<Outline>().enabled = false;
        }
    }


}


