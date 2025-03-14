using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{

    [Header("Ingredients")]
    [SerializeField] public float water;
    [SerializeField] public float milk;
    [SerializeField] public float foam;
    [SerializeField] public float chocolate_syrup;
    [SerializeField] public int shot;
    [SerializeField] public int ice;




    void DefineCoffe()
    {
        DefineQuality();
    }

    void DefineQuality()
    {

    }
}
