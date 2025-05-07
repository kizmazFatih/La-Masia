using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoButton : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] private ParticleSystem coffe;

    public void Interact(Transform handle)
    {
        Transform cup = transform.parent.GetChild(0).GetChild(0);
        cup.GetComponent<Cup>().shot += 1;
        StartCoroutine(FillCoffe());
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


    IEnumerator FillCoffe()
    {
        coffe.gameObject.SetActive(true);
        coffe.Play();
        yield return new WaitForSeconds(2f);
        coffe.Stop();
        coffe.gameObject.SetActive(false);

    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }


}
