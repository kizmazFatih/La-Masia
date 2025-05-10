using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMachine : Machines, IInteractable
{
    [SerializeField] private Transform milkButton;
    public void Interact(Transform handle)
    {
        if (cupPlace.childCount > 0 && handle.childCount == 0)
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
                    CameraSwitcher.instance.SwitchCamera(3);
                    ScriptsManager.instance.GoFree();
                    milkButton.GetComponent<Outline>().enabled = true;
                }

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
            product.GetComponent<Cup>().ice += 2;
            product.GetComponent<Product>().work = true;
            product.transform.gameObject.layer = 0;
            CameraSwitcher.instance.SwitchCamera(3);
            ScriptsManager.instance.GoFPS();
            milkButton.GetComponent<Outline>().enabled = false;

        }
    }


}
