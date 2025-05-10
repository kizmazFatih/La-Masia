using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cup_prefab;
    [SerializeField] private Canvas myCanvas;

    public void Interact(Transform handle)
    {
        if (handle.childCount != 0) return;
        GameObject new_cup = Instantiate(cup_prefab, handle.position, Quaternion.Euler(-90, 0, -50));
        new_cup.transform.SetParent(handle);
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
