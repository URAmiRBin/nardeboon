using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreEvents {
    public static Action onCurrentLevelWin;
    public static Action onCurrentLevelLose;
}

public class CoreGameManager : MonoBehaviour, ICore {
    int _level;

    void Awake() {
        // TODO: Load from saves
        _level = 1;
    }

    void OnEnable() {
        CoreEvents.onCurrentLevelWin += WinLevel;
        CoreEvents.onCurrentLevelLose += LoseLevel;
    }

    void OnDisable() {
        CoreEvents.onCurrentLevelWin -= WinLevel;
        CoreEvents.onCurrentLevelLose -= LoseLevel;
    }

    public void Initialize() {
        HookButtons();
    }

    void HookButtons() {
        UIManager.Instance.Elements.reviveButton.onClick.AddListener(Revive);
        UIManager.Instance.Elements.nextLevelButton.onClick.AddListener(() => StartLevel(++_level));
        UIManager.Instance.Elements.retryButton.onClick.AddListener(ReplayLevel);    
    }

    public void StartGame() {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.AGREED, 0) == 1) {
            GameEvents.onStateChange(GameStates.MainMenu);
        } else {
            UIManager.Instance.ShowAgreements();
        }
    }

    public void ExitGame() {}

    public void ReplayLevel() {
        AdManager.Instance.ShowInterstitial(RestartLevel, RestartLevel);
    }

    public void WinLevel() {
        GameEvents.onLevelWin?.Invoke(_level);    
    }

    public void LoseLevel() {
        GameEvents.onLevelLose?.Invoke(_level);
    }

    public void Revive() {}
    public void FreezeGame() {}
    public void StartLevel(int level) {
        SceneManager.LoadScene(1);
        GameEvents.onStateChange?.Invoke(GameStates.MainMenu);
    }

    public void RestartLevel() => StartLevel(_level);    

    public void GetLevelReward() {

    }
}
