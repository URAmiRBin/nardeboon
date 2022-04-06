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
        _hasItem = EconomyManager.Instance.HasItem(_config);
    }
    
    public void FillData(ItemBase itemBase) {
        itemBase.SetCallback();
        _config = itemBase.config;
        _image.sprite = _config.sprite;
        _button.onClick.AddListener(BuyOrUseItem);
    }

    void BuyOrUseItem() {
        if (_hasItem) {
            _config.useCallback.Invoke();
        } else {
            if (EconomyManager.Instance.CanSpend(_config.cost)) {
                GameEvents.onCurrencySpend(_config.cost);
                EconomyManager.Instance.AddToInventory(_config);    
                _hasItem = true;   
            } else {
                UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
            }
        }
    }
}
