using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviourSingletion<EconomyManager> {
    int coinAmount;
    List<ItemConfig> items = new List<ItemConfig>();

    void Awake() {
        coinAmount = PlayerPrefs.GetInt(PlayerPrefKeys.COIN, 0);

        // TODO: Load items from save
    }

    void OnEnable() {
        GameEvents.onCurrencyEarn += AddCoin;
        GameEvents.onCurrencySpend += SpendCoin;
    }

    void OnDisable() {
        GameEvents.onCurrencyEarn -= AddCoin;
        GameEvents.onCurrencySpend -= SpendCoin;
    }

    public bool CanSpend(int amount) => coinAmount >= amount;

    void SetCoin(int amount) {
        if (amount < 0) return;
        coinAmount = amount;
        UIManager.Instance.Elements.coin.text = amount.ToString();
    }

    void AddCoin(int addedAmount) => SetCoin(coinAmount + addedAmount);
    void SpendCoin(int spentAmount) => SetCoin(coinAmount - spentAmount);

    public void AddToInventory(ItemConfig item) => items.Add(item);
    
    public bool HasItem(ItemConfig itemConfig) {
        for(int i = 0; i < items.Count; i++) {
            if (itemConfig.name == items[i].name) return true;
        }
        return false;
    }
}
