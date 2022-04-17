using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIElement {
    // [SerializeField] [Range(.01f, .99f)] float disabledOptionAlpha = 0.25f;
   
    //FIXME: How about switch?? it's not button
    public ToggleController vibrationButton;
    public ToggleController soundButton; 

    public Button noAdsButton;
    public Button privacyButton;

    bool _vibrationState, _soundState;
    // Image _vibrationImage, _soundImage;
    // Color _onColor, _offColor;

    void Awake() {
        _vibrationState = PlayerPrefs.GetInt(PlayerPrefKeys.VIBRATION) == 1 ? true : false;
        SetVibrationState(_vibrationState);
        // _vibrationImage = vibrationButton.GetComponent<Image>();
        // _onColor = _vibrationImage.color;
        // _offColor = new Color(_onColor.r, _onColor.g, _onColor.b, disabledOptionAlpha);
        // _vibrationImage.color = _vibrationState ? _onColor : _offColor;

        // TODO: Sound
        // TODO: NoAds
    }

    void Start() {
        vibrationButton.SetAction(SetVibrationState);
        soundButton.SetAction(SetSoundState);
        // FIXME: Handle privacyButton assignment in here UI Manager should be initialized like other services
    }

    void SetVibrationState(bool state) {
        _vibrationState = state;
        NardeboonEvents.UIEvents.onVibrationSetEvent?.Invoke(_vibrationState);
        vibrationButton.isOn = _vibrationState;
        // _vibrationImage.color = _vibrationState ? _onColor : _offColor;
    }

    void SetSoundState(bool state) {
        _soundState = state;
        NardeboonEvents.UIEvents.onSoundSetEvent?.Invoke(_soundState);
        soundButton.isOn = _soundState;
    }
}
