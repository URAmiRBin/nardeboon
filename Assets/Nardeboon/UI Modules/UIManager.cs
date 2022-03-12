using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingletion<UIManager> {
    [SerializeField] GameStates defaultState;
    [SerializeField] UIMaps maps;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Popup popupInstance;
    [SerializeField] UIElements elements;

    static Popup popup;
    GameStates currentState;
    UIElement currentPanel;
    public UIElements Elements { get => elements; }

    void Awake() {
        UpdateState(defaultState);
        popup =  Instantiate(popupInstance, transform);
        DontDestroyOnLoad(this);

        elements.startGame.SetCallback(() => UpdateState(GameStates.Gameplay));
    }

    void OnEnable() {
        GameEvents.onStateChange += UpdateState;
        GameEvents.onLevelWin += SetLevelText;
    }

    void OnDisable() {
        GameEvents.onStateChange -= UpdateState;
        GameEvents.onLevelWin -= SetLevelText;
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

    void SetLevelText(int level) {
        foreach(Text levelText in elements.levelTexts) {
            levelText.text = "LEVEL " + (level + 1).ToString();
        }
    }

    void LevelStart() => UpdateState(GameStates.MainMenu);

    // FIXME: Don't use static, Jamasb told
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
