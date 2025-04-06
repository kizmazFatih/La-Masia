using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]

public class Product : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    private Canvas myCanvas;
    public bool work = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCanvas = transform.GetChild(0).GetComponent<Canvas>();
    }

    public void Interact(Transform handle)
    {
        if (!work) return;
        if (handle.childCount == 0)
        {
            Take(handle);
        }

    }

    public void Take(Transform handle)
    {
        if (!work) return;
        rb.isKinematic = true;
        transform.parent = handle;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Release(Transform handle)
    {

        rb.isKinematic = false;
        transform.parent = null;
    }

    public Canvas ShowMyUI()
    {
        if (!work) return null;
        myCanvas.gameObject.SetActive(true);
        return myCanvas;
    }


}
