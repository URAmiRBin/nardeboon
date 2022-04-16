using UnityEngine;

public interface Collectable {
    void Collect(int amount = 1);
    void Use(int amount = 1);
}

[System.Serializable]
public class InventoryItem : Collectable {
    [SerializeField] GameItem _item;
    [SerializeField] int amount;

    public int Amount { get => amount; }
    public string Name { get => _item.name; }
    public UnityEngine.Sprite Sprite { get => _item.image; }
    public int Price { get => _item.cost * amount; }

    public InventoryItem(GameItem item, int amount = 1) {
        _item = item;
        this.amount = amount;
    } 

    public void Collect(int amount = 1) => this.amount += amount;

    public void Use(int amount = 1) {
        if (amount > this.amount) throw new System.InvalidOperationException();
        this.amount -= amount;
        _item.Use();
    }
}

[System.Serializable]
public enum GameItemValue {
    Common, Rare, Epic
}