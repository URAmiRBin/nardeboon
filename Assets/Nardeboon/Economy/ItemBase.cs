using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    [SerializeField] protected ItemConfig _config;

    public virtual void Use() {}
}
