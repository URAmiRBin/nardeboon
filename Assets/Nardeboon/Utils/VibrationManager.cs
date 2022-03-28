using UnityEngine;

public class VibrationManager {
    private long _shortVibrationTimeInMilliseconds = 50;
    private long _longVibrationTimeInMilliseconds = 150;
    private bool _vibration = false;

    public VibrationManager(long shortVibrationDurationInMilliseconds, long longVibrationDurationInMilliseconds) {
        _shortVibrationTimeInMilliseconds = shortVibrationDurationInMilliseconds;
        _longVibrationTimeInMilliseconds = longVibrationDurationInMilliseconds;
        Vibration.Init();
        UIEvents.onVibrationSetEvent += SetVibrationStatus;
    }

    ~VibrationManager() {
        UIEvents.onVibrationSetEvent -= SetVibrationStatus;
    }

    public bool VibrationStatus {
        get => _vibration;
        private set {
            _vibration = value;
            PlayerPrefs.SetInt("VIBRATION", _vibration ? 1 : 0);
        }
    }
 
    void SetVibrationStatus(bool status) => VibrationStatus = status;

    public void Vibrate(long vibrationDuration) {
        if (!_vibration) return;
        // Cancel previous vibration to avoid annoying multi vibrations
        Vibration.Cancel();
        Vibration.Vibrate(vibrationDuration);
    }

    public void ShortVibrate() => Vibrate(_shortVibrationTimeInMilliseconds);
    public void LongVibrate() => Vibrate(_longVibrationTimeInMilliseconds);
}
