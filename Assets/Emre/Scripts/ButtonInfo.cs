using UnityEngine;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TMP_Text PriceTxt;
    public TMP_Text QuantityTxt;
    public GameObject ShopManager;

    void Start()
    {
        PriceTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString() + "  TL";
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}
