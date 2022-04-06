using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour {
    Image _image;
    Button _button;

    void Awake() {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    
    public void FillData(ItemConfig itemConfig) {
        _image.sprite = itemConfig.sprite;
        // TODO: Check for money
        _button.onClick.AddListener(() => itemConfig.useCallback());
    }
}
