using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItem : MonoBehaviour {
    public PowerupConfig config;
    public void SetCallback() => config.useCallback += Use;
    public void Use(int level) {Debug.Log(level);}
}
