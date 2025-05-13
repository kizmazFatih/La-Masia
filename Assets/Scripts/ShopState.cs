using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("End of Day Screen")]
    [SerializeField] private GameObject dayEndScreen;

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
        EventDispatcher.SummonEvent("SetCustomerCount", popularity);

        if (dayEndScreen != null)
            dayEndScreen.SetActive(false);
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

        if (clock == 24)
        {
            dayContinue = false;
            isOpen = false;
            Time.timeScale = 0;
            DayEnd();
        }
    }

    void DayEnd()
    {
        if (dayEndScreen != null)
        {
            dayEndScreen.SetActive(true);
        }
    }

    public void StartNewDay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
