using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            GameEvents.onStateChange?.Invoke(GameStates.Splash);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            GameEvents.onStateChange?.Invoke(GameStates.Lose);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            GameEvents.onStateChange?.Invoke(GameStates.Win);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            GameEvents.onStateChange?.Invoke(GameStates.MainMenu);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            UIManager.ShowPopup("HI THERE!");
        }
    }
}
