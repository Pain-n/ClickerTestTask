using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataModel
{
    public float Balance;
    public List<BuisnessData> BuisnessesData = new List<BuisnessData>();
}

[Serializable]
public class BuisnessData 
{
    public int ID;
    public int LvL;
    public bool[] Upgrades = new bool[2];
    public float ProfitProgress;
}

