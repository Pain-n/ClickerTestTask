using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buisness : MonoBehaviour
{
    public static event Action<float> SendProfit;

    public Slider ProgressBar;
    int ID;

    int Lvl;
    int BaseProfit;
    int BasePrice;
    float FirstUbgradeMultipuier;
    float SecondUbgradeMultipuier;

    public int lvl => Lvl;
    public int id => ID;
    public float firstUbgradeMultipuier => FirstUbgradeMultipuier;
    public float secondUbgradeMultipuier => SecondUbgradeMultipuier;

    public void SetUpValues(int _id, int _Lvl, int _BaseProfit, int _BasePrice)
    {
        ID = _id;
        Lvl = _Lvl;
        BaseProfit = _BaseProfit;
        BasePrice = _BasePrice;
    }

    void Update()
    {
        if (Lvl > 0) Tick();
    }

    void Tick()
    {
        if (ProgressBar.value == ProgressBar.maxValue)
        {
            SendProfit?.Invoke(CalculateProfit());
            ProgressBar.value = 0;
        }
            ProgressBar.value += Time.deltaTime;
    }
    public float CalculateProfit()
    {
        float profit = Lvl * BaseProfit * (1 + FirstUbgradeMultipuier + SecondUbgradeMultipuier);
        return profit;
    }

    public int CalculateLvlPrice()
    {
        int price = (Lvl + 1) * BasePrice;
        return price;
    }

    public void LvlUp()
    {
        Lvl += 1;
    }

    public void SetUpgrade(int id, int value)
    {
        if (id == 1)
        {
            FirstUbgradeMultipuier = value * 0.01f;
        }
        else
        {
            SecondUbgradeMultipuier = value * 0.01f;
        }
    }
}
