using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] GameStates defaultState;
    [SerializeField] UIMaps maps;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Popup popup;
    GameStates currentState;
    UIElement currentPanel;

    void Awake() => UpdateState(defaultState);
    
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

        if (Input.GetKeyDown(KeyCode.P)) {
            ShowPopup("HI THERE MF!");
        }
    }
    
    void UpdateState(GameStates state) {
        if (state == currentState) return;
        currentPanel?.Close();
        currentState = state;
        try {
            currentPanel = maps[state.ToString()] as UIElement;
            currentPanel.Open();
            backgroundPanel.SetActive(currentPanel.hasBackground);
        } catch (NullReferenceException) {
            Debug.LogWarning("State " + state.ToString() + " is not defined.");
        }
    }

    void ShowPopup(string message) {
        // TODO: Connect to animation system
        if (popup.IsActive) ClosePopup();
        popup.AssemblePopup(message);
        popup.Open();
    }

    void ClosePopup() {
        popup.Close();
    }
}
