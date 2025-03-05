using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Machines, IInteractable
{
    public void Interact(Transform handle)
    {
        Destroy(handle.GetChild(0).gameObject);
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

    // Start is called before the first frame update

}
