using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdConfig {
    public bool isTestBuild;

    [Header("Admob")]
    public bool useAdmob;
    public string admobAppID;
    public AdUnits admobUnits;

    [Header("UnityAds")]
    public bool useUnityAds;
    public string unityAdsAppID;
    public AdUnits unityAdUnits;
}

[System.Serializable]
public class AdUnits : ReflectableClass {
    public string banner, interstitial, rewarded;
}