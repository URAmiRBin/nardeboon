using System.Collections.Generic;
using UnityEngine;
using System;

public class AdManager : MonoBehaviour {
    [SerializeField] AdService bannerProvider;
    public readonly int maxTries = 3;
    List<AdService> services;

    static AdManager _instance;
    List<AdService> _rewardedServices, _interstitialServices;
    int _rewardedNextServiceIndex, _interestialNextServiceIndex;
    bool _bypassForceAds;

    public static AdManager Instance { get => _instance; }

    public bool IsRewardedReady {
        get {
            for (int i = 0; i < _rewardedServices.Count; i++) {
                if (_rewardedServices[i].IsRewardedReady) return true;
            }
            return false;
        }
    }

    void Awake() {
        if (_instance != null) Destroy(this);
        _instance = this;
        DontDestroyOnLoad(this);
        _interstitialServices = new List<AdService>();
        _rewardedServices = new List<AdService>();

        // TODO: Load from your own save system whether no ads has been purchased or not
        _bypassForceAds = PlayerPrefs.GetInt("NoAds", 0) != 0;
    }

    public void BuildServices(AdConfig adConfig) {
        services = new List<AdService>();
        if (adConfig.useAdmob) {
            var admobAdService = gameObject.AddComponent<AdmobAdService>();
            admobAdService.SetUnitIds(adConfig.admobUnits);
            services.Add(admobAdService);
        }
        if (adConfig.useUnityAds) {
            var unityAdsService = gameObject.AddComponent<UnityAdService>();
            unityAdsService.SetUnitIds(adConfig.unityAdUnits);
            services.Add(unityAdsService);
        }
    }

    public void InitializeAds(bool testMode) {
        for (int i = 0; i < services.Count; i++) {
            if (!services[i].excludeInterstital) _interstitialServices.Add(services[i]);
            if (!services[i].excludeRewarded) _rewardedServices.Add(services[i]);
            services[i].Initialize(testMode);
        }
        bannerProvider = services[0] ?? null;
    }

    public void ShowInterstitial(Action success = null, Action fail = null) {
        if (_bypassForceAds) return;
        for (int i = 0; i < _interstitialServices.Count; i++) {
            int clampedIndex = ListUtils.ClampListIndex(i + _interestialNextServiceIndex, _interstitialServices.Count);
            if (_interstitialServices[clampedIndex].IsInterstitialReady) {
                _interstitialServices[clampedIndex].ShowInterstitial(success, fail);
                _interestialNextServiceIndex = clampedIndex + 1;
                return;
            }
        }
        fail?.Invoke();
    }

    public void ShowRewarded(Action success, Action fail) {
        // TODO: Show error message
        for (int i = 0; i < _rewardedServices.Count; i++) {
            int clampedIndex = ListUtils.ClampListIndex(i + _rewardedNextServiceIndex, _rewardedServices.Count);
            if (_rewardedServices[clampedIndex].IsRewardedReady) {
                _rewardedServices[clampedIndex].ShowRewarded(success, fail);
                _rewardedNextServiceIndex = clampedIndex + 1;
                return;
            }
        }
        fail?.Invoke();
    }

    public void ShowBanner() {
        if (_bypassForceAds) return;
        bannerProvider.ShowBanner();
    }
    public void DestroyBanner() => bannerProvider.HideBanner();

    public void BypassForceAds() {
        _bypassForceAds = true;
        DestroyBanner();
    }

}
