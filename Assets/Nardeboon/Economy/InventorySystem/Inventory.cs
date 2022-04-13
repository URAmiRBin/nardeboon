using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviourSingletion<Inventory> {
    [SerializeField] GameItem currency;
    public List<InventoryItem> _items = new List<InventoryItem>();
    
    private int Wallet { get => _items[0].Amount; }

    void Awake() {
        _items.Add(new InventoryItem(currency));
        // TODO: Load inventory from save
    }

    public void AddToInventory(InventoryItem item) {
        if (item == null || item.Price < 0) return;
        int index = _items.FindIndex(i => i.Name == item.Name);
        if (index == -1) _items.Add(item);
        else _items[index].Collect(item.Amount);
    }

    public void AddToInventory(int amount) => _items[0].Collect(amount);

    public void BuyItem(InventoryItem item) {
        try {
            _items[0].Use(item.Price);
            AddToInventory(item);
        } catch (System.InvalidOperationException) {
            Debug.Log("You don't have enough money to buy " + item.Name);
        }
    }
}