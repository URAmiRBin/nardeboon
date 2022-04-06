using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupState {
    Purchasable, WatchAd, MaxedOut
}

public class PowerupItemUI : MonoBehaviour {
    [SerializeField] PowerupItem powerupItem;
    [SerializeField] GameObject priceContainer, watchAdContainer, maxedContainer;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    [SerializeField] Button powerupButton;
    [SerializeField] Image powerupImage;
    int maxLevel;
    PowerupState _currentState;
    int _level;
    PowerupConfig _config;

    void Awake() {
        FillData(powerupItem);
        powerupButton.onClick.AddListener(Upgrade);

        // TODO: Load level and cost from save
        _level = 1;
    }

    void Start() {
        UpdateState();
    }

    void FillData(PowerupItem item) {
        item.SetCallback();
        _config = item.config;
        maxLevel = _config.cost.Length;
        powerupImage.sprite = _config.sprite;
    }

    // TODO: Refresh state on panel open

    PowerupState GetState() {
        if (_level == maxLevel) return PowerupState.MaxedOut; 
        if (EconomyManager.Instance.CanSpend(_config.cost[_level - 1])) return PowerupState.Purchasable;
        else return PowerupState.WatchAd;
    }

    public void UpdateState() {
        priceText.text = _config.cost[_level - 1].ToString();
        levelText.text = "LVL " + _level.ToString();        
        _currentState = GetState();
        DisableAll();
        switch (_currentState) {
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
                GameEvents.onCurrencySpend(_config.cost[_level - 1]);
                _level++;
                UpdateState();
                _config.useCallback?.Invoke(_level);
                break;
            case PowerupState.WatchAd:
                AdManager.Instance.ShowRewarded(
                    () => {
                        // TODO: Get this from curve
                        _level++;
                        UpdateState();
                        _config.useCallback?.Invoke(_level);  
                    },
                    () => UIManager.ShowPopup("No internet")
                );
                break;
            case PowerupState.MaxedOut:
                break;
        }
    }
}
