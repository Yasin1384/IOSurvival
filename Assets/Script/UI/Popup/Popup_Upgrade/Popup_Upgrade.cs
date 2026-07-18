using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Upgrade : Popup
{
    private const int MAX_TOWERS = 5;
    [SerializeField] private Button upgradeButton;
    private ItemCardsTowerData_SO currentData;

    public override void Setup()
    {
        if (CurrencyManager.Instance.coins < currentData.Price)
        {
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
        }

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(()=>
        {
            OnCardClicked(currentData);
        });
    }


    private void OnCardClicked(ItemCardsTowerData_SO data)
    {
        currentData = data;

        if (currentData == null || currentData.TowerBehaviorData == null)
        {
            return;
        }

        if (SelectedCardsHolder.SelectedTowers.Count >= MAX_TOWERS)
        {
            return;
        }

        SelectedCardsHolder.SelectedTowers.Add(currentData.TowerBehaviorData);
        CurrencyManager.Instance.SpendCoins(data.Price);
        SaveTowers();
    }

    private void SaveTowers()
    {
        List<string> ids = new List<string>();

        foreach (var tower in SelectedCardsHolder.SelectedTowers)
        {
            ids.Add(tower.Name);
        }

        SpawnTowerData data = new SpawnTowerData(ids);

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("SelectedTowers", json);
        PlayerPrefs.Save();
    }
}
