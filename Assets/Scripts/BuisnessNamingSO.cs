using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuisnessNaming", menuName = "ScriptableObjects/BuisnessNamingSO")]
public class BuisnessNamingSO : ScriptableObject
{
    [SerializeField]
    List<BuisnessNamingListModel> _BuisnessNamingList;

    public List<BuisnessNamingListModel> BuisnessNamingList => _BuisnessNamingList;
}

[Serializable]
public class BuisnessNamingListModel
{
    [SerializeField]
    string _BuisnessName;
    [SerializeField]
    List<string> _UpgradeNames;

    public string BuisnessName => _BuisnessName;
    public List<string> UpgradeNames => _UpgradeNames;
}
