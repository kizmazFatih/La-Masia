using DG.Tweening;
using UnityEngine;

public class FoamButton : MonoBehaviour
{
    private bool is_open = false;
    private Transform cup;

    [SerializeField] private Transform indicator;
    [SerializeField] private MilkButton milkButton;
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
                cup = transform.parent.GetChild(1).GetChild(0);
                transform.DOLocalMoveZ(originalPos.z - 0.05f, 0.5f).SetEase(Ease.OutQuad);

                if (!is_open)
                {
                    is_open = true;
                    InvokeRepeating(nameof(MakeFoam), 0f, 0.2f);
                }
                else
                {
                    is_open = false;
                    CancelInvoke(nameof(MakeFoam));
                }
            }
            else if (Input.GetMouseButtonUp(0) && hit.transform == transform)
            {
                transform.DOLocalMoveZ(originalPos.z, 0.5f).SetEase(Ease.OutQuad);
            }
        }
        milkButton.enabled = !is_open;
    }

    void MakeFoam()
    {
        if (cup.GetComponent<Cup>().milk <= 0)
        {
            indicator.localPosition = new Vector3(0.1f, 0, -4f);
            CancelInvoke(nameof(MakeFoam));
            is_open = false;
        }

        SetIndicatorPosition();
        cup.GetComponent<Cup>().foam += 0.2f;
        cup.GetComponent<Cup>().milk -= 0.2f;
    }



    void SetIndicatorPosition()
    {
        float amount = cup.GetComponent<Cup>().foam;
        float targetPosition = amount - 4;
        if (targetPosition <= -4.1f || targetPosition >= 2.5f)
        {
            CancelInvoke(nameof(MakeFoam));
            is_open = false;
        }
        indicator.localPosition = new Vector3(0.1f, 0, targetPosition);
    }
}
