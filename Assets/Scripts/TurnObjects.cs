using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnObjects : MonoBehaviour
{
    public Transform cup;
    [SerializeField] private ParticleSystem water;





    [Header("Faucet variables")]
    float first_x;
    float delta;
    public float total_rotation;

    private void Start()
    {
        water.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            first_x = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {


            delta = Input.mousePosition.x - first_x;
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
        //cup.GetComponent<Cup>().water += total_rotation == 0 ? (delta / 720) * Time.deltaTime : (total_rotation / 720) * Time.deltaTime;
        if (total_rotation != 0)
        {
            cup.GetComponent<Cup>().water += (total_rotation / 720) * Time.deltaTime;
            water.gameObject.SetActive(true);
            water.Play();
            water.startSize = total_rotation / (720 * 5);

        }
        else
        {
            water.Stop();
            water.gameObject.SetActive(false);
            cup.GetComponent<Cup>().water += (delta / 720) * Time.deltaTime;
        }

    }




}

