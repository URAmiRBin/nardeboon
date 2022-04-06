using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPopulator : MonoBehaviour {
    [SerializeField] ShopItemUI defaultItemPrefab;
    [SerializeField] Transform contentsParent;
    [SerializeField] ItemBase[] shopItems;

    void Start() {
        Debug.Log(shopItems.Length);
        foreach(ItemBase item in shopItems) {
            Debug.Log("HEY");
            Instantiate(defaultItemPrefab, contentsParent).FillData(item.config);
        }
    }
}
