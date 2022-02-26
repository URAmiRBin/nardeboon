using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDouble : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount >= 1) {
            GameEvents.onLevelStart?.Invoke("0");
            Debug.Log("Send on level start event");
        }
    }
}
