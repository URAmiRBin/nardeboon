using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeLevelProgressIndicator : LevelProgressIndicator {
    [SerializeField] Sprite[] _themeSprites;
    Image _currentThemeImage, _nextThemeImage;

    protected override void Awake()
    {
        cap = transform.childCount - 2;
        progressImages = new Image[cap];
        _currentThemeImage = transform.GetChild(0).GetComponent<Image>();
        _nextThemeImage = transform.GetChild(cap + 1).GetComponent<Image>();
        for(int i = 0; i < cap; i++) {
            progressImages[i] = transform.GetChild(i + 1).GetComponent<Image>();
        }
    }

    public override void SetLevel(int level) {
        base.SetLevel(level);
        int themeIndex = (level - 1) / cap;
        _currentThemeImage.sprite = _themeSprites[themeIndex % _themeSprites.Length];
        _nextThemeImage.sprite = _themeSprites[(themeIndex + 1) % _themeSprites.Length];
    }
}
