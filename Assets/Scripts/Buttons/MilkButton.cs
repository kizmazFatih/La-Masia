using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MilkButton : MonoBehaviour
{
    public bool is_open = false;
    private Transform cup;

    [SerializeField] private Transform indicator;
    [SerializeField] private FoamButton foamButton;
    [SerializeField] private ParticleSystem milkEffect;



    private Vector3 originalPos;


    void Start()
    {
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out RaycastHit hit, 100f))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform == transform)
            {
                transform.DOLocalMoveZ(originalPos.z - 0.05f, 0.5f).SetEase(Ease.OutQuad);

                cup = transform.parent.GetChild(1).GetChild(0);

                if (!is_open)
                {
                    is_open = true;
                    CancelInvoke(nameof(FillMilk));
                    InvokeRepeating(nameof(FillMilk), 0f, 0.2f);
                    milkEffect.gameObject.SetActive(true);

                }
                else
                {
                    is_open = false;
                    CancelInvoke(nameof(FillMilk));
                    milkEffect.gameObject.SetActive(false);
                }
            }
            else if (Input.GetMouseButtonUp(0) && hit.transform == transform)
            {
                transform.DOLocalMoveZ(originalPos.z, 0.5f).SetEase(Ease.OutQuad);
            }
        }
        foamButton.enabled = !is_open;
    }

    void FillMilk()
    {
        if (cup.GetComponent<Cup>().milk <= 6.5f)
        {
            cup.GetComponent<Cup>().milk += 0.2f;
            SetIndicatorPosition();
        }
        else
        {
            CancelInvoke(nameof(FillMilk));
            is_open = false;
            milkEffect.gameObject.SetActive(false);
        }
    }


    void SetIndicatorPosition()
    {
        float amount = cup.GetComponent<Cup>().milk;
        float targetPosition = amount - 4;

        if (targetPosition <= -4.1f || targetPosition >= 2.5f)
        {
            CancelInvoke(nameof(FillMilk));
            is_open = false;
            milkEffect.gameObject.SetActive(false);
        }
        indicator.localPosition = new Vector3(0.1f, 0, targetPosition);


    }
}
