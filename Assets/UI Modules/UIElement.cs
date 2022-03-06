using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : MonoBehaviour {
    public bool hasBackground;
    public bool IsActive {get; protected set;}

    public virtual void Open() {
        IsActive = true;
        foreach(Transform child in transform) child.gameObject.SetActive(true);
    }
    public virtual void Close() {
        IsActive = false;
        foreach(Transform child in transform) child.gameObject.SetActive(false);
    }
}