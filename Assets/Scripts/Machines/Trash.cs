using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Machines, IInteractable
{
    public void Interact(Transform handle)
    {
        if (handle.GetChild(0).tag == "Product")
        { Destroy(handle.GetChild(0).gameObject); }
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

    // Start is called before the first frame update

}
