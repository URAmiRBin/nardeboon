using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour {
    [SerializeField] Image _image;
    Button _button;
    [SerializeField] GameObject _priceContainer;
    [SerializeField] Text _priceText;
    bool _hasItem;
    ItemConfig _config;
    int indexInMenu;
    ShopItemPopulator shopMenu;

    void Awake() {
        _button = GetComponent<Button>();
        _hasItem = EconomyManager.Instance.HasItem(_config);
    }
    
    public void FillData(ItemBase itemBase, int index, ShopItemPopulator populator) {
        indexInMenu = index;
        shopMenu = populator;
        itemBase.SetCallback();
        _config = itemBase.config;
        _image.sprite = _config.sprite;
        _priceText.text = _config.cost.ToString();
        _button.onClick.AddListener(BuyOrUseItem);
        _priceContainer.SetActive(!_hasItem);
    }

    void BuyOrUseItem() {
        if (_hasItem) {
            _config.useCallback?.Invoke();
            shopMenu.Select(indexInMenu);
        } else {
            if (EconomyManager.Instance.CanSpend(_config.cost)) {
                GameEvents.onCurrencySpend(_config.cost);
                EconomyManager.Instance.AddToInventory(_config);    
                _hasItem = true;  
                _priceContainer.SetActive(!_hasItem);
            } else {
                UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
            }
        }
    }

    public void ChangeState(bool isHighlight) {
        GetComponent<Image>().color = isHighlight ? Color.grey : Color.white;
    }
}
