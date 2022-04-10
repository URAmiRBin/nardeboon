using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdConfig {
    [HideInInspector] public bool isTestBuild;
    public AdIterationType iterationType;
    public AdServiceConfig[] adServices;
}

[System.Serializable]
public class AdUnits : ReflectableClass {
    public string banner, interstitial, rewarded;
}

[System.Serializable]
public enum AdNetwork {
    Admob, Unity,
}

[System.Serializable]
public enum AdIterationType {
    Absolute, Loop
}

[System.Serializable]
public class AdServiceConfig {
    public AdNetwork network;
    public string appID;
    public AdUnits units;
}