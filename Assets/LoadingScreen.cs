using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    [SerializeField] ProgressBar progressBar;
    
    bool _isLoading;
    
    public bool LoadingState => _isLoading;

    public void SetProgress(float percent) {
        progressBar.SetProgress(percent);
        // TODO: Connect to UI animation system
        if (percent >= 1f) gameObject.SetActive(false);
    }
}
