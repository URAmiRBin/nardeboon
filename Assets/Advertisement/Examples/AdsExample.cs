using UnityEngine;

public class AdsExample : MonoBehaviour {
    void Start() => AdManager.Instance.InitializeAds();
    public void ShowInterstitial() => AdManager.Instance.ShowInterstitial(null, null);
    public void ShowRewarded() => AdManager.Instance.ShowRewarded(() => Debug.Log("Reward Gained"), () => Debug.Log("No Rewards"));
    public void ShowBanner() => AdManager.Instance.ShowBanner();
    public void HideBanner() => AdManager.Instance.DestroyBanner();
}
