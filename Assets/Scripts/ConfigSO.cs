using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/ConfigSO")]
public class ConfigSO : ScriptableObject
{
    [SerializeField]
    List<BuisnessModelForConfig> _BuisnessList;

    public List<BuisnessModelForConfig> BuisnessList => _BuisnessList;
}

[Serializable]
public class BuisnessModelForConfig 
{
    [SerializeField]
    int _ID;
    [SerializeField]
    int _ProfitDelayInSeconds;
    [SerializeField]
    int _BasePrice;
    [SerializeField]
    int _BaseProfit;
    [SerializeField]
    List<BuisnessUpgrade> _BuisnessUpgradeList;

    public int ID => _ID;
    public int ProfitDelayInSeconds => _ProfitDelayInSeconds;
    public int BasePrice => _BasePrice;
    public int BaseProfit => _BaseProfit;
    public List<BuisnessUpgrade> BuisnessUpgradeList => _BuisnessUpgradeList;
}

[Serializable]
public class BuisnessUpgrade
{
    [SerializeField]
    int _Price;
    [SerializeField]
    int _ProfitMultiplier;

    public int Price => _Price;
    public int ProfitMultiplier => _ProfitMultiplier;
}

