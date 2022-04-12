using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemConfig {
    public string name;
    public Sprite sprite;
    public int cost;
    [HideInInspector] public System.Action useCallback;
}