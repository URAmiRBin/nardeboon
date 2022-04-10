using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreDouble : MonoBehaviour {
    void Awake() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            CoreEvents.onCurrentLevelWin?.Invoke();
        }
    }
}
