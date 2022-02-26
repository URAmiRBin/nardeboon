using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour {
    // More analytics APIs might be added later
    [SerializeField] bool useAnalytics;
    [SerializeField] GameObject gameAnalytics;

    // Dependencies
    AnalyticsSystem analyticsSystem;
    
    void Awake() {
        if (useAnalytics) {
            Instantiate(gameAnalytics);
            analyticsSystem = new GameAnalyticsSystem();
            analyticsSystem.Initialize();
        }

        //TODO: Do the loading progression here
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
    }

    void OnDestroy() {
        analyticsSystem.Destroy();
    }
}
