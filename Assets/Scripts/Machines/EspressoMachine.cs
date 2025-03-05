using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EspressoMachine : Machines, IInteractable
{


    public void Interact(Transform handle)
    {

        if (handle.childCount != 0)
        {
            product = handle.GetChild(0);
            if (product.tag == "Product")
            {
                if (cupPlace.childCount == 0)
                {
                    product.parent = cupPlace;
                    product.position = cupPlace.position;
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


    public void DoMyJob(Transform handle)
    {
        if (cupPlace.childCount >= 0)
        {
            //
        }
    }

    public override void MyGame()
    {
        base.MyGame();
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
