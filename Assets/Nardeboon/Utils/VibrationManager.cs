using UnityEngine;

public class VibrationManager {
    private static long _shortVibrationTimeInMilliseconds = 50;
    private static long _longVibrationTimeInMilliseconds = 150;
    private static bool _vibration = false;

    VibrationManager(long shortVibrationDurationInMilliseconds, long longVibrationDurationInMilliseconds) {
        _shortVibrationTimeInMilliseconds = shortVibrationDurationInMilliseconds;
        _longVibrationTimeInMilliseconds = longVibrationDurationInMilliseconds;
        Vibration.Init();
        UIEvents.OnVibrationSetEvent += SetVibrationStatus;
    }

    ~VibrationManager() {
        UIEvents.OnVibrationSetEvent -= SetVibrationStatus;
    }

    public static bool VibrationStatus {
        get => _vibration;
        private set {
            _vibration = value;
            PlayerPrefs.SetInt(PrefsKeyManager.vibration, IntBoolConverter.BoolToInt(_vibration));
        }
    }
 
    void SetVibrationStatus(bool status) => VibrationStatus = status;

    public static void Vibrate(long vibrationDuration) {
        if (!_vibration) return;
        // Cancel previous vibration to avoid annoying multi vibrations
        Vibration.Cancel();
        Vibration.Vibrate(vibrationDuration);
    }

    public static void ShortVibrate() => Vibrate(_shortVibrationTimeInMilliseconds);
    public static void LongVibrate() => Vibrate(_longVibrationTimeInMilliseconds);
}
