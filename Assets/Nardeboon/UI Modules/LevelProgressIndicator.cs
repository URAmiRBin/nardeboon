using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ProgressIndicatorType {
    Default, Boss, Theme
}

[System.Serializable]
public class ProgressionIndicatorMaps : ReflectableClass {
    public GameObject defaultPrefab;
    public GameObject bossPrefab;
    public GameObject themePrefab;
}

public class LevelProgressIndicator : MonoBehaviour {
    [SerializeField] Sprite passedSprite, notPassedSprite;

    protected int cap;
    protected Image[] progressImages;

    public virtual LevelProgressIndicator Initialize(UIConfig config) {
        passedSprite = config.passedSprite;
        notPassedSprite = config.notPassedSprite;
        return this;
    }

    protected virtual void Awake() {
        cap = transform.childCount;
        progressImages = new Image[cap];
        progressImages = transform.GetComponentsInChildren<Image>(); 
    }

    public virtual void SetLevel(int level) {
        level = (level - 1) % cap;
        for (int i = 0; i < cap; i++) {
            progressImages[i].sprite = i > level ? notPassedSprite : passedSprite;
        }
    }
}
