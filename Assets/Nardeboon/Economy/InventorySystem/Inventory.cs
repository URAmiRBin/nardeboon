using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviourSingletion<Inventory> {
    [SerializeField] GameItem currency;
    public List<InventoryItem> _items = new List<InventoryItem>();
    
    public int Wallet { get => _items[0].Amount; }
    public string MainCurrency { get => _items[0].Name; }

    void Awake() {
        // TODO: Load
        if (_items.Count == 0) _items.Add(new InventoryItem(currency));
    }

    public void AddToInventory(InventoryItem item) {
        if (item == null || item.Price < 0) return;
        int index = _items.FindIndex(i => i.Name == item.Name);
        if (index == -1) _items.Add(item);
        else if (index == 0) AddToInventory(item.Amount);
        else _items[index].Collect(item.Amount);
    }

    public bool HasItemWithName(string name) => _items.Exists(x => x.Name == name);

    public void AddToInventory(int amount) {
        GameEvents.onCurrencyEarn?.Invoke(amount);
        Runner.UIManager.UpdateCoin();
        _items[0].Collect(amount);
    }

    public void BuyItem(InventoryItem item) {
        try {
            _items[0].Use(item.Price);
            GameEvents.onCurrencySpend?.Invoke(item.Price);
            Runner.UIManager.UpdateCoin();
            AddToInventory(item);
        } catch (System.InvalidOperationException e) {
            throw e;
        }
    }
}