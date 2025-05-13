using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopState : MonoBehaviour
{
    public static ShopState instance;


    [Header("Day Circle")]
    public float time;
    public float hourLength;
    public int clock;
    public bool isOpen = false;
    private bool dayContinue = false;




    [Header("Shop Specials")]

    [Range(0, 100)] public float popularity;
    [SerializeField] private int level = 0;

    [Header("UI's")]
    [SerializeField] private TextMeshProUGUI clockText;





    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        dayContinue = true;

        //Bu satır vize içindir sonradan silinecek
        EventDispatcher.SummonEvent("SetCustomerCount", popularity);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isOpen = !isOpen;
        }

        if (dayContinue)
        {
            time += 1 * Time.deltaTime;


            clockText.text = clock.ToString() + ":" + time.ToString("00");
            if (time >= hourLength)
            {
                clock += 1;
                time = 0f;
                EventDispatcher.SummonEvent("SetCustomerCount", popularity);
            }
        }
        if (clock == 20)
        {
            dayContinue = false;
            isOpen = false;
            Time.timeScale = 0;
            DayEnd();

        }
    }


    void DayEnd()
    {
        //Yeni sahneye geç
        //kazandığımız para
        //Rastgele eventlerle paramız azalacak
        //İkinci güne başla
        //Para,popoularity kaydet
    }


}
