using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    public ItemConfig config;

    void Awake() => config.useCallback = Use;

    public virtual void Use() {}
}
