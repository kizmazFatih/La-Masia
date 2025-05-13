using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnObjects : MonoBehaviour
{
    public Transform cup;
    public ParticleSystem water;
    [SerializeField] private Transform indicator;





    [Header("Faucet variables")]
    private float first_x;
    private float delta;
    public float total_rotation;
    public bool isWaterOn = false;

    private void Start()
    {
        water.Stop();
    }

    private void Update()
    {

        if (isWaterOn) { SetIndicatorPosition(); }


        if (Input.GetMouseButtonDown(0))
        {
            first_x = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {


            delta = Input.mousePosition.x - first_x;
            if (delta != 0) isWaterOn = false;
            if ((total_rotation + delta <= 0 && delta < 0) || (total_rotation + delta >= 720 && delta > 0))
            {
                return;
            }
            else
            {
                transform.rotation = Quaternion.Euler(total_rotation + delta, 90, -90);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            total_rotation += delta;
            total_rotation = Mathf.Clamp(total_rotation, 0, 720);
            delta = 0;
        }

        if (cup == null) return;
        if (total_rotation != 0)
        {

            cup.GetComponent<Cup>().water += (total_rotation / 720) * Time.deltaTime;

            isWaterOn = true;
            water.gameObject.SetActive(true);
            water.Play();

            water.startSize = total_rotation / (720 * 5);

        }
        else
        {
            isWaterOn = false;
            water.Stop();
            water.gameObject.SetActive(false);

            cup.GetComponent<Cup>().water += (delta / 720) * Time.deltaTime;
        }



    }

    void SetIndicatorPosition()
    {
        float amount = cup.GetComponent<Cup>().water;
        float targetPosition = amount - 4;

        if (amount >= 7)
        {
            total_rotation = 0;
            water.Stop();
            isWaterOn = false;
            water.gameObject.SetActive(false);
            this.enabled = false;

        }

        indicator.localPosition = new Vector3(0.1f, 0, targetPosition);
    }




}

