using TMPro;
using UnityEngine;

public class RandomEventsManager : MonoBehaviour
{

    public static RandomEventsManager instance;

    private int randomNumber;


    [Header("Revenues")]
    [SerializeField] private Transform revenueContent;


    [Header("Expenses")]
    [SerializeField] private Transform expenseContent;


    private void Awake()
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


    public void WriteCoin()
    {
        revenueContent.GetChild(0).GetComponent<TextMeshProUGUI>().text += CoinSystem.instance.coin.ToString();
        expenseContent.GetChild(0).GetComponent<TextMeshProUGUI>().text += -50;

        randomNumber = Random.Range(0, 11);

        if (randomNumber < 3)
        {
            SelectRandomEvent(revenueContent);
        }
        else if (randomNumber > 7)
        {
            SelectRandomEvent(expenseContent);
        }

    }




    void SelectRandomEvent(Transform content)
    {

        int randomEventCount = Random.Range(0, 100);
        if (randomEventCount < 10) randomEventCount = 2;
        else if (randomEventCount > 96) randomEventCount = 3;
        else randomEventCount = 1;


        for (int i = 0; i < randomEventCount; i++)
        {
            int randomText = Random.Range(1, content.childCount);
            int randomMoney = Random.Range(0, 120);
            content.GetChild(randomText).gameObject.SetActive(true);
            content.GetChild(randomText).GetComponent<TextMeshProUGUI>().text += randomMoney.ToString();
        }

    }
}
