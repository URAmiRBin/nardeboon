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
    InventoryItem _item;
    int indexInMenu;
    ShopItemPopulator shopMenu;

    bool CanBuyItem {
        get => _item.IsConsumable || !Inventory.Instance.HasItemWithName(_item.Name);
    }

    void Awake() {
        _button = GetComponent<Button>();
    }

    
    public void FillData(InventoryItem item, int index, ShopItemPopulator populator) {
        _item = item;
        indexInMenu = index;
        shopMenu = populator;
        _image.sprite = item.Sprite;
        _priceText.text = item.Price.ToString();
        _button.onClick.AddListener(BuyItem);
        _priceContainer.SetActive(CanBuyItem);
    }

    void BuyItem() {
        if (!CanBuyItem) return;
        try {
            Inventory.Instance.BuyItem(_item);
            _priceContainer.SetActive(CanBuyItem);
        } catch {
            UIManager.ShowPopup("Not enough money", UIManager.ClosePopup, yesText: "OK");
        }
    }

    public void ChangeState(bool isHighlight) {
        GetComponent<Image>().color = isHighlight ? Color.grey : Color.white;
    }
}
