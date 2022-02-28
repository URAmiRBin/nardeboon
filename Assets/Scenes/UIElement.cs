using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : MonoBehaviour {
    public bool hasBackground;
    public bool IsActive {get; private set;}

    public virtual void Open() {
        IsActive = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public virtual void Close() {
        IsActive = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}