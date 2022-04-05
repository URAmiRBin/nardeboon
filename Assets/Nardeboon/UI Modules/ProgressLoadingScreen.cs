using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLoadingScreen : MonoBehaviour {
    [SerializeField] Image fillImage;

    public void SetProgress(float percent) {
        fillImage.fillAmount = percent;
        if (percent >= 1f) FinishProgress();
    }

    public void StartProgress() {
        foreach(Transform child in transform)
            child.gameObject.SetActive(true);
        fillImage.fillAmount = 0;
    }

    public void FinishProgress() {
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt(PlayerPrefKeys.AGREED, 0) == 1) {
            GameEvents.onStateChange(GameStates.MainMenu);
        } else {
            UIManager.ShowPopup("Agreement text", () => {
                PlayerPrefs.SetInt(PlayerPrefKeys.AGREED, 1);
                UIManager.ClosePopup();
                GameEvents.onStateChange(GameStates.MainMenu);
            }
                , yesText: "I Agree");
        }
    }
}
