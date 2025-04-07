using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour, IInteractable
{
    private Animator animator;

    public void Interact(Transform handle)
    {
        Transform cup = transform.parent.GetChild(0).GetChild(0);
        cup.GetComponent<Cup>().shot += 1;
        animator.SetTrigger("Clicked");
    }

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Canvas ShowMyUI()
    {
        return null;
    }


    void Start()
    {
        animator = GetComponent<Animator>();
    }


}
