using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Nardeboon/ItemConfig", order = 1)]
public class ItemConfig : ScriptableObject {
    public new string name;
    public Sprite sprite;
    public int cost;
    [HideInInspector] public System.Action useCallback;
}
