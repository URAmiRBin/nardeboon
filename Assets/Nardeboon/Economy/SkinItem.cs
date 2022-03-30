using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinItem : ItemBase {
    public override void Use() {
        Debug.Log(base._config.name + " used!");
    }
}
