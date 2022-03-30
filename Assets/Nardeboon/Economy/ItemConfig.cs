using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Nardeboon/ItemConfig", order = 1)]
public class ItemConfig : ScriptableObject {
    [SerializeField] new string name;
}
