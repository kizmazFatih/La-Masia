using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public float water, milk, foam, chocolate_syrup;
    public int shot, ice;
    public CoffeType myCoffe = CoffeType.Other;
    public CoffeeSize myCoffeSize;

    public struct CoffeRecipe
    {
        public float Water;
        public float Milk;
        public float Foam;
        public float ChocolateSyrup;
        public int Shot;
        public int Ice;

        public CoffeRecipe(float water, float milk, float foam, float chocolateSyrup, int shot, int ice)
        {
            Water = water;
            Milk = milk;
            Foam = foam;
            ChocolateSyrup = chocolateSyrup;
            Shot = shot;
            Ice = ice;
        }
    }

    private Dictionary<CoffeType, CoffeRecipe> coffeRecipes = new Dictionary<CoffeType, CoffeRecipe>()
    {
        { CoffeType.Espresso,       new CoffeRecipe(0, 0, 0, 0, 1, 0) },
        { CoffeType.Americano,      new CoffeRecipe(5, 0, 0, 0, 1, 0) },
        { CoffeType.IcedAmericano,  new CoffeRecipe(5, 0, 0, 0, 2, 5) },
        { CoffeType.Latte,          new CoffeRecipe(0, 7, 3, 0, 1, 0) },
        { CoffeType.IcedLatte,      new CoffeRecipe(0, 7, 3, 0, 1, 5) },
        { CoffeType.Cappuccino,     new CoffeRecipe(0, 6, 4, 0, 1, 0) },
        { CoffeType.IcedCappuccino, new CoffeRecipe(0, 6, 4, 0, 1, 5) },
        { CoffeType.Mocha,          new CoffeRecipe(0, 7, 0, 2, 1, 0) },
        { CoffeType.IcedMocha,      new CoffeRecipe(0, 7, 0, 2, 1, 5) },
        { CoffeType.Macchiato,      new CoffeRecipe(0, 0, 3, 0, 1, 0) },
        { CoffeType.FlatWhite,      new CoffeRecipe(0, 5, 0, 0, 1, 0) },
        { CoffeType.Cortado,        new CoffeRecipe(0, 2, 0, 0, 1, 0) }
    };



    void DefineCoffe()
    {
        float liquid_tolerance = 1.5f; // ±2 birim hata payı
        int thick_tolerance = 2;

        foreach (var recipe in coffeRecipes)
        {
            CoffeRecipe correctRecipe = recipe.Value;

            float value = water + milk + foam + chocolate_syrup + shot + ice;
            if (value == 0) return;
            if (IsWithinTolerance(correctRecipe, liquid_tolerance, thick_tolerance))
            {
                myCoffe = recipe.Key;
                Debug.Log("Coffe: " + myCoffe + " Score: " + CalculateMatchScore(correctRecipe));
                return;
            }
        }

        myCoffe = CoffeType.Other;

    }


    bool IsWithinTolerance(CoffeRecipe correctRecipe, float tolerance, int tolerance2)
    {
        return Mathf.Abs(correctRecipe.Water - water) <= tolerance &&
               Mathf.Abs(correctRecipe.Milk - milk) <= tolerance &&
               Mathf.Abs(correctRecipe.Foam - foam) <= tolerance &&
               Mathf.Abs(correctRecipe.ChocolateSyrup - chocolate_syrup) <= tolerance &&
               Mathf.Abs(correctRecipe.Shot - shot) <= tolerance2 &&
               Mathf.Abs(correctRecipe.Ice - ice) <= tolerance2;
    }

    float CalculateMatchScore(CoffeRecipe correctRecipe)
    {
        float score = 100f;

        score -= Mathf.Abs(correctRecipe.Water - water) * 5;
        score -= Mathf.Abs(correctRecipe.Milk - milk) * 5;
        score -= Mathf.Abs(correctRecipe.Foam - foam) * 5;
        score -= Mathf.Abs(correctRecipe.ChocolateSyrup - chocolate_syrup) * 5;
        score -= Mathf.Abs(correctRecipe.Shot - shot) * 10;
        score -= Mathf.Abs(correctRecipe.Ice - ice) * 5;

        return Mathf.Max(0, score); //Puan 0'dan küçük olamaz
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Dezgah")
        {
            DefineCoffe();
            EventDispatcher.SummonEvent("IsReadyMyOrder");
        }
    }

}
