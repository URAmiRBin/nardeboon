using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour {
    [SerializeField] Image _image;
    
    public void FillData(ItemConfig itemConfig) {
        _image.sprite = itemConfig.sprite;
    }
}
