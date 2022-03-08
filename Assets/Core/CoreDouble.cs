using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDouble : MonoBehaviour {
    int level = 1;

    void Awake() {
        UIManager.Instance.Elements.nextLevelButton.onClick.AddListener(() => Debug.Log("jdsa"));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount >= 1) {
            GameEvents.onLevelStart?.Invoke(level.ToString());
            UIManager.Instance.Elements.levelProgressIndicator.SetLevel(level++);
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
