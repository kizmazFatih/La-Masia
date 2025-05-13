using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public static CoinSystem instance;

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
    }

    public void AddCoin(int amount)
    {
        coin += amount;
    }
}
