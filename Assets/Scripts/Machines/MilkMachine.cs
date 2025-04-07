using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMachine : Machines, IInteractable
{
    public void Interact(Transform handle)
    {
        throw new System.NotImplementedException();
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
