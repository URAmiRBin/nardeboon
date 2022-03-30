using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdConfig {
    public bool isTestBuild;
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
public class AdServiceConfig {
    public AdNetwork network;
    public string appID;
    public AdUnits units;
}