using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressIndicator : MonoBehaviour {
    [SerializeField] Sprite passedSprite, notPassedSprite;

    int cap;
    public Image[] progressImages;

    void Awake() {
        cap = transform.childCount;
        progressImages = new Image[cap];
        progressImages = transform.GetComponentsInChildren<Image>();  
    }

    public void SetLevel(int level = 0) {
        level = level % cap;
        for (int i = 0; i < cap; i++) {
            progressImages[i].sprite = i > level ? notPassedSprite : passedSprite;
        }
    }
}
