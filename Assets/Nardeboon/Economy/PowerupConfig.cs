using UnityEngine;

[System.Serializable]
public class PowerupConfig {
    public string name;
    public Sprite sprite;
    public int[] cost;
    [HideInInspector] public System.Action<int> useCallback;
}