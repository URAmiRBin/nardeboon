using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    GameStates currentState = GameStates.Splash;
    UIElement currentPanel;
    [SerializeField] UIMaps maps;

    void Start() => currentPanel = maps[GameStates.MainMenu.ToString()] as UIElement;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            UpdateState(GameStates.Lose);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            UpdateState(GameStates.Win);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            UpdateState(GameStates.MainMenu);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            UpdateState(GameStates.Splash);
        }
    }
    
    void UpdateState(GameStates state) {
        if (state == currentState) return;
        currentPanel.Close();
        currentState = state;
        currentPanel = maps[state.ToString()] as UIElement;
        currentPanel.Open();
    }
}

