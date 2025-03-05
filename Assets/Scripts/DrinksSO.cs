using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksSO : ScriptableObject
{

    [Header("Recipe")]
    public float milk_amount;
    public float sugar_amount;
    public float coffee_amount;
    public float water_amount;
    public float ice_amount;

    [Header("Price")]
    public float price;

    

}
