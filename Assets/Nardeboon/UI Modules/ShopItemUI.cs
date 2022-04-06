using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour {
    Image _image;
    Button _button;
    bool _hasItem;
    ItemConfig _config;

    void Awake() {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        // TODO: Check inventory
        _hasItem = EconomyManager.Instance.HasItem(_config);
    }
    
    public void FillData(ItemConfig itemConfig) {
        _config = itemConfig;
        _image.sprite = itemConfig.sprite;
        _button.onClick.AddListener(BuyOrUseItem);
    }

    void BuyOrUseItem() {
        if (_hasItem) {
            _config.useCallback?.Invoke();
        } else {
            if (EconomyManager.Instance.CanSpend(_config.cost)) {
                GameEvents.onCurrencySpend(_config.cost);
                EconomyManager.Instance.AddToInventory(null);       
            } else {
                UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
            }
        }
    }
}
