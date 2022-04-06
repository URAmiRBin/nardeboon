using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupState {
    Purchasable, WatchAd, MaxedOut
}

public class PowerupItemUI : MonoBehaviour {
    [SerializeField] GameObject priceContainer, watchAdContainer, maxedContainer;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    [SerializeField] Button powerupButton;
    PowerupState _currentState;

    void Awake() {
        powerupButton.onClick.AddListener(Upgrade);
    }

    public void UpdateState(PowerupState state) {
        _currentState = state;
        DisableAll();
        switch (state) {
            case PowerupState.Purchasable:
                priceContainer.SetActive(true);
                break;
            case PowerupState.WatchAd:
                watchAdContainer.SetActive(true);
                break;
            case PowerupState.MaxedOut:
                maxedContainer.SetActive(true);
                break;
        }
    }

    void DisableAll() {
        priceContainer.SetActive(false);
        watchAdContainer.SetActive(false);
        maxedContainer.SetActive(false);
    }

    void Upgrade() {
        switch (_currentState) {
            case PowerupState.Purchasable:
                // TODO: Reduce money and activate
                break;
            case PowerupState.WatchAd:
                // TODO: Watch ad and activate
                break;
            case PowerupState.MaxedOut:
                break;
        }
    }
}
