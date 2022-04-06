using UnityEngine;


[CreateAssetMenu(fileName = "PowerupConfig", menuName = "Nardeboon/PowerupConfig", order = 2)]
public class PowerupConfig : ScriptableObject {
    public new string name;
    public Sprite sprite;
    public int[] cost;
    [HideInInspector] public System.Action<int> useCallback;
}