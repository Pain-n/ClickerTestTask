using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem
{
    public static void SaveGame(List<Buisness> buisnesses, float balance)
    {
        DataModel model = new DataModel();
        model.Balance = balance;

        foreach (Buisness buisness in buisnesses)
        {
            BuisnessData buisnessData = new BuisnessData();
            buisnessData.ID = buisness.id;
            buisnessData.LvL = buisness.lvl;
            if(buisness.firstUbgradeMultipuier != 0) buisnessData.Upgrades[0] = true;
            if(buisness.secondUbgradeMultipuier != 0) buisnessData.Upgrades[1] = true;
            buisnessData.ProfitProgress = buisness.ProgressBar.value;
            model.BuisnessesData.Add(buisnessData);
        }

        string data = JsonUtility.ToJson(model);

        File.WriteAllText(Application.persistentDataPath + "/ClickerData.json", data);
    }

    public static DataModel LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/ClickerData.json") == false) return null;
        string data = File.ReadAllText(Application.persistentDataPath + "/ClickerData.json");
        return JsonUtility.FromJson<DataModel>(data);
    }
}
