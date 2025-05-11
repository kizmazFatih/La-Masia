using System;
using System.Collections;
using UnityEngine;

public class MilkButton : MonoBehaviour
{
    private Animator animator;
    private bool is_open = false;
    private Transform cup;
    [SerializeField] private Transform indicator;



    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //SetIndicatorPosition();
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out RaycastHit hit, 100f))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform == transform)
            {
                cup = transform.parent.GetChild(1).GetChild(0);


                if (!is_open)
                {
                    is_open = true;
                    InvokeRepeating(nameof(FillMilk), 0f, 0.3f);
                }
                else
                {
                    is_open = false;
                    CancelInvoke(nameof(FillMilk));
                }
            }
        }
    }

    void FillMilk()
    {
        cup.GetComponent<Cup>().milk += 0.1f;
    }

    void SetIndicatorPosition()
    {
        float amount = cup.GetComponent<Cup>().milk;
        indicator.localPosition = new Vector3(amount, 0, 0);
    }
}
