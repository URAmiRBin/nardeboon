using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressIndicator : MonoBehaviour {
    [SerializeField] Sprite passedSprite, notPassedSprite;

    protected int cap;
    public Image[] progressImages;

    protected virtual void Awake() {
        Debug.Log("HEYA");
        cap = transform.childCount;
        progressImages = new Image[cap];
        progressImages = transform.GetComponentsInChildren<Image>(); 
    }

    void Start() => SetLevel(0);

    public virtual void SetLevel(int level) {
        level = (level - 1) % cap;
        for (int i = 0; i < cap; i++) {
            progressImages[i].sprite = i > level ? notPassedSprite : passedSprite;
        }
    }
}
