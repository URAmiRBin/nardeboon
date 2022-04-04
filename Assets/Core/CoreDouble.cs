using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreDouble : MonoBehaviour {
    int level = 1;

    void Awake() {
        UIManager.Instance.Elements.nextLevelButton.onClick.AddListener(() => GameEvents.onLevelWin(level++));
        UIManager.Instance.Elements.nextLevelButton.onClick.AddListener(() => GameEvents.onStateChange(GameStates.MainMenu));
        UIManager.Instance.Elements.retryButton.onClick.AddListener(() => GameEvents.onStateChange(GameStates.MainMenu));
        UIManager.Instance.Elements.retryButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        UIManager.Instance.Elements.reviveButton.onClick.AddListener(() => AdManager.Instance.ShowRewarded(() => GameEvents.onStateChange(GameStates.Gameplay), () => UIManager.ShowPopup("NO INTERNET BIATCH")));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount >= 1) {
            GameEvents.onLevelStart?.Invoke(level);
        }
    
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
            Runner.vibrationManager.ShortVibrate();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            AdManager.Instance.ShowInterstitial();
        } if (Input.GetKeyDown(KeyCode.R)) {
            AdManager.Instance.ShowRewarded(() => Debug.Log("THIS IS YOUR REWARD!!"), null);
        } if (Input.GetKeyDown(KeyCode.B)) {
            AdManager.Instance.ShowBanner();
        }
    }
}
