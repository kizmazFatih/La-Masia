using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ParticleSystem coffe;
    private bool filling = false;



    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out RaycastHit hit, 100f))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform == transform)
            {
                Transform cup = transform.parent.GetChild(1).GetChild(0);

                if (!filling)
                {
                    cup.GetComponent<Cup>().shot += 1;
                    StartCoroutine(FillCoffe());
                    animator.SetTrigger("Clicked");
                }
            }
        }
    }

    IEnumerator FillCoffe()
    {
        filling = true;
        coffe.gameObject.SetActive(true);
        coffe.Play();

        yield return new WaitForSeconds(2f);

        coffe.Stop();
        coffe.gameObject.SetActive(false);
        filling = false;


    }




}
