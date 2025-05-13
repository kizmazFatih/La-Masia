using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public static CoinSystem instance;
    [SerializeField] private TextMeshProUGUI coinText;

    private int coin;


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

        coinText.text = coin.ToString();
    }



    public void AddCoin(int amount)
    {
        coin += amount;
        coinText.text = coin.ToString();
    }
}
