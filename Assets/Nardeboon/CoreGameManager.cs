using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreGameManager : MonoBehaviour, ICore {
    int _level;

    void Awake() {
        // TODO: Load from saves
        _level = 1;
    }

    public void Initialize() {
        HookButtons();
    }

    void HookButtons() {
        UIManager.Instance.Elements.reviveButton.onClick.AddListener(ReplayLevel);
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

    public void LoseLevel() {}
    public void Revive() {}
    public void FreezeGame() {}
    public void StartLevel(int level) {}

    public void RestartLevel() => StartLevel(_level);    

    public void GetLevelReward() {

    }
}
