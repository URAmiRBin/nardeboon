using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] GameStates defaultState;
    [SerializeField] UIMaps maps;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Popup popupInstance;

    static Popup popup;
    GameStates currentState;
    UIElement currentPanel;

    void Awake() {
        UpdateState(defaultState);
        popup =  Instantiate(popupInstance, transform);
    }

    void OnEnable() => GameEvents.onStateChange += UpdateState;
    void OnDisable() => GameEvents.onStateChange -= UpdateState;
    
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

    void LevelStart() => UpdateState(GameStates.MainMenu);

    public static void ShowPopup(string message) {
        // TODO: Connect to animation system
        if (popup.IsActive) ClosePopup();
        popup.AssemblePopup(message);
        popup.Open();
    }

    public static void ClosePopup() {
        popup.Close();
    }
}
