using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI BalanceText;
    [SerializeField]
    Transform BuisnessUIContainer;
    [SerializeField]
    ConfigSO Config;
    [SerializeField]
    BuisnessNamingSO BuisnessNaming;

    List<Buisness> BuisnessDataList = new List<Buisness>();

    float Balance;

    void Start()
    {
        Buisness.SendProfit += TakeProfit;
        StartGame();
    }
    void StartGame()
    {
        DataModel GameData = SaveLoadSystem.LoadGame();

        for(int i = 0; i< Config.BuisnessList.Count; i++)
        {
            UIBuisnessPanel BuisnessPanel = Instantiate(Resources.Load<UIBuisnessPanel>("Prefabs/UIBuisnessPanel"), BuisnessUIContainer);
            BuisnessPanel.ProgressBar.maxValue = Config.BuisnessList[i].ProfitDelayInSeconds;
            BuisnessPanel.BuisnessName.text = BuisnessNaming.BuisnessNamingList[i].BuisnessName;

            Buisness BuisnessComponent = BuisnessPanel.GetComponent<Buisness>();
            BuisnessDataList.Add(BuisnessComponent);

            BuisnessPanel.FirstUpgradeButtonText.text = "��������� 1" + "\n" + "�����: + " + Config.BuisnessList[i].BuisnessUpgradeList[0].ProfitMultiplier + "%" + "\n" + "����: " + Config.BuisnessList[i].BuisnessUpgradeList[0].Price + "$";
            BuisnessPanel.SecondUpgradeButtonText.text = "��������� 2" + "\n" + "�����: + " + Config.BuisnessList[i].BuisnessUpgradeList[1].ProfitMultiplier + "%" + "\n" + "����: " + Config.BuisnessList[i].BuisnessUpgradeList[1].Price + "$";

            //Setting up values
            if (GameData == null)
            {
                if (i == 0)
                {
                    BuisnessComponent.SetUpValues(Config.BuisnessList[i].ID, 1, Config.BuisnessList[i].BaseProfit, Config.BuisnessList[i].BasePrice);
                }
                else
                {
                    BuisnessComponent.SetUpValues(Config.BuisnessList[i].ID, 0, Config.BuisnessList[i].BaseProfit, Config.BuisnessList[i].BasePrice);
                }
            }
            else
            {
                Balance = GameData.Balance;
                BalanceText.text = "������: " + Balance + "$";

                BuisnessData buisnessData = null;
                foreach(BuisnessData item in GameData.BuisnessesData)
                {
                    if(item.ID == Config.BuisnessList[i].ID)
                    {
                        buisnessData = item;
                    }
                }

                BuisnessComponent.SetUpValues(buisnessData.ID, buisnessData.LvL, Config.BuisnessList[i].BaseProfit, Config.BuisnessList[i].BasePrice);

                if (buisnessData.Upgrades[0] != false)
                {
                    BuisnessComponent.SetUpgrade(1, Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].ProfitMultiplier);
                    BuisnessPanel.FirstUpgradeButtonText.text = "��������� 1" + "\n" + "�����: + " + Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].ProfitMultiplier + "\n" + "�������";
                    BuisnessPanel.FirstUpgradeButton.interactable = false;
                }
                if(buisnessData.Upgrades[1] != false)
                {
                    BuisnessComponent.SetUpgrade(2, Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].ProfitMultiplier);
                    BuisnessPanel.SecondUpgradeButtonText.text = "��������� 2" + "\n" + "�����: + " + Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].ProfitMultiplier + "\n" + "�������";
                    BuisnessPanel.SecondUpgradeButton.interactable = false;
                }
                BuisnessComponent.ProgressBar.value = buisnessData.ProfitProgress;
            }
            BuisnessPanel.LvlText.text = "LvL " + "\n" + BuisnessComponent.lvl;
            BuisnessPanel.LvlUpButtonText.text = "LvLUP" + "\n" + "����: " + BuisnessComponent.CalculateLvlPrice() + "$";
            BuisnessPanel.ProfitText.text = "�����: " + BuisnessComponent.CalculateProfit();

            //Button logic
            BuisnessPanel.LvlUpButton.onClick.AddListener(() =>
            {
                int LvlPrice = BuisnessComponent.CalculateLvlPrice();
                if(Balance >= LvlPrice)
                {
                    Balance -= LvlPrice;
                    BuisnessComponent.LvlUp();
                    BuisnessPanel.LvlText.text = "LvL " + "\n" + BuisnessComponent.lvl;
                    BuisnessPanel.LvlUpButtonText.text = "LvLUP" + "\n" + "����: " + BuisnessComponent.CalculateLvlPrice() + "$";
                    BuisnessPanel.ProfitText.text = "�����: " + BuisnessComponent.CalculateProfit() + "$";
                    BalanceText.text = "������: " + Balance + "$";
                }
            });

            BuisnessPanel.FirstUpgradeButton.onClick.AddListener(() =>
            {
                if(Balance >= Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].Price && BuisnessComponent.lvl > 0)
                {
                    Balance -= Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].Price;
                    BuisnessComponent.SetUpgrade(1, Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].ProfitMultiplier);
                    BuisnessPanel.FirstUpgradeButtonText.text = "��������� 1" + "\n" + "�����: + " + Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[0].ProfitMultiplier + "\n" + "�������";
                    BuisnessPanel.ProfitText.text = "�����: " + BuisnessComponent.CalculateProfit() + "$";
                    BalanceText.text = "������: " + Balance + "$";
                    BuisnessPanel.FirstUpgradeButton.interactable = false;
                }
            });

            BuisnessPanel.SecondUpgradeButton.onClick.AddListener(() =>
            {
                if (Balance >= Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].Price && BuisnessComponent.lvl > 0)
                {
                    Balance -= Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].Price;
                    BuisnessComponent.SetUpgrade(2, Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].ProfitMultiplier);
                    BuisnessPanel.SecondUpgradeButtonText.text = "��������� 2" + "\n" + "�����: + " + Config.BuisnessList[BuisnessComponent.id].BuisnessUpgradeList[1].ProfitMultiplier + "\n" + "�������";
                    BuisnessPanel.ProfitText.text = "�����:" + BuisnessComponent.CalculateProfit() + "$";
                    BalanceText.text = "������: " + Balance + "$";
                    BuisnessPanel.SecondUpgradeButton.interactable = false;
                }
            });
        }
    }

    void TakeProfit(float profit)
    {
        Balance += profit;
        BalanceText.text = "������: " + Balance + "$";
    }

    private void OnDisable()
    {
        Buisness.SendProfit -= TakeProfit;
        SaveLoadSystem.SaveGame(BuisnessDataList, Balance);
    }
}
