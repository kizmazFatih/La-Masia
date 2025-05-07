using System;
using System.Collections;
using UnityEngine;

public class MilkButton : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool is_open = false;
    private Transform cup;

    public void Interact(Transform handle)
    {
        cup = transform.parent.GetChild(0).GetChild(0);
        
        if (!is_open)
        {
            is_open = true;
            InvokeRepeating(nameof(FillMilk), 0f, 0.3f);
        }
        else
        {
            Debug.Log("safaasf");
            is_open = false;
            CancelInvoke(nameof(FillMilk));
        }
        
    }

  

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Canvas ShowMyUI()
    {
        return null;
    }

    void FillMilk()
    {
        cup.GetComponent<Cup>().milk += 0.1f;
    }

    void Start()
    {
        //animator = GetComponent<Animator>();
    }
}
