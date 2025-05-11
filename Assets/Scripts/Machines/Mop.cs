using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mop : MonoBehaviour, IInteractable
{

    private Rigidbody rb;
    private Canvas myCanvas;

    private Transform handled_object;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCanvas = transform.GetChild(0).GetComponent<Canvas>();
        handled_object = null;
    }

    void Update()
    {
        if (handled_object == null) return;
        if (handled_object.childCount > 0 && handled_object.GetChild(0) == this.transform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Yer Siliniyor");
            }
        }
    }

    public void Interact(Transform handle)
    {

        if (handle.childCount == 0)
        {
            Take(handle);
            handled_object = handle;
        }

    }

    public void Take(Transform handle)
    {
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

    public Transform ShowMyUI()
    {
        myCanvas.gameObject.SetActive(true);
        return myCanvas.transform.parent;
    }


}
