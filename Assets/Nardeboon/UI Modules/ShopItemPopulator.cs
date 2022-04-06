using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPopulator : MonoBehaviour {
    [SerializeField] ShopItemUI defaultItemPrefab;
    [SerializeField] Transform contentsParent;
    [SerializeField] ItemBase[] shopItems;

    void Start() {
        foreach(ItemBase item in shopItems) {
            Instantiate(defaultItemPrefab, contentsParent).FillData(item.config);
        }
    }
}
