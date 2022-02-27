using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    [SerializeField] Image fillImage;

    void Awake() {
        if (fillImage == null) fillImage = GetComponent<Image>();
    }

    public void SetProgress(float percent) {
        fillImage.fillAmount = percent;
    }
}
