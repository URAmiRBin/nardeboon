using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIConfig {
    public string privacyURL;
    [Multiline] public string agreementsText;
    public TutorialType tutorialFingerType;
    public UIManager uiManagerPrefab;

    [Header("Levels Progress")]
    public bool showLevelText;
    public ProgressIndicatorType progressIndicatorType;
    public Sprite passedSprite, notPassedSprite;
    public Sprite[] themeSprites;
    public Sprite bossSpritePassed, bossSpriteNotPassed;
    
}
