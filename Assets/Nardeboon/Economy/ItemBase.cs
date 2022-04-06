using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    public ItemConfig config;

    void Awake() => config.useCallback = Use;

    public virtual void Use() {}

    public void Buy() {
        if (EconomyManager.Instance.CanSpend(config.cost)) {
            GameEvents.onCurrencySpend(config.cost);

            // TODO: Add to inventory
        } else {
            UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
        }
    }
}
