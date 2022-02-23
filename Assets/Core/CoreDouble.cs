using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDouble : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameEvents.onLevelStart?.Invoke("0");
        }
    }
}
