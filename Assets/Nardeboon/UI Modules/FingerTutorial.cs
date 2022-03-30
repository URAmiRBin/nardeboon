using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum TutorialType {
    None = 0, Swipe = 1, Tap = 2,
}

public class FingerTutorial : MonoBehaviour {
    [SerializeField] Text _guideText;
    [SerializeField] Animator _fingerAnimator;
    TutorialType _tutorialType = TutorialType.None;

    public void Initialize(TutorialType type) => _tutorialType = type;

    void OnEnable() => SetupTutorial();

    void SetupTutorial() {
        if (_tutorialType == TutorialType.None) {
            Debug.LogWarning("No tutorial type is set, make sure to initialize!");
            return;
        }
        _guideText.text = _tutorialType.ToString().ToUpper() + " TO START!";
        _fingerAnimator.SetInteger("type", (int)_tutorialType);
    }
}
