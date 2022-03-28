using UnityEngine;

public class VibrationManager : MonoBehaviour {
    private static long _vibrationTimeInMilliseconds = 50;
    private static bool _vibration = false;

    void Awake() => Vibration.Init();
    
    public static bool VibrationStatus {
        get => _vibration;
        private set {
            _vibration = value;
            PlayerPrefs.SetInt(PrefsKeyManager.vibration, IntBoolConverter.BoolToInt(_vibration));
        }
    }
    
    void OnEnable() => UIEvents.OnVibrationSetEvent += SetVibrationStatus;
    void OnDisable() => UIEvents.OnVibrationSetEvent -= SetVibrationStatus;

    void SetVibrationStatus(bool status) => VibrationStatus = status;

    public static void Vibrate() {
        if (!_vibration) return;
        // Cancel previous vibration to avoid annoying multi vibrations
        Vibration.Cancel();
        Vibration.Vibrate(_vibrationTimeInMilliseconds);
    }
}
