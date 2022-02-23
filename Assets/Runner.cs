using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {
    [SerializeField] CoreDouble core;

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
        Instantiate(core);
    }

    void OnDestroy() {
        analyticsSystem.Destroy();
    }
}
