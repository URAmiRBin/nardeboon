using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnalyticsConfig {
    public bool useAnalytics;

    [Header("GameAnalytics")]
    public GameObject gameAnalytics;
    public string gameAnalyticsGameKey;
    public string gameAnalyticsSecretKey;

    [Header("Adjust")]
    public GameObject adjustPrefab;
    public string adjustToken;
}
