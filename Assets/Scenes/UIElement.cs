using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : MonoBehaviour {
    public bool hasBackground;

    public virtual void Open() {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public virtual void Close() {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}