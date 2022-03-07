# A comprehensive ad manager for Unity

This is a ad manager system for Unity that handles multiple ad services and loops through available ad services to show different types of ads.


## How to use

- Install the package and select desired ad services.
- Drag and drop Ad Manager prefab in Paycheque/Examples into the first scene.
- Modify Ad Manager services as you wish.
- To show ads:
    - Call `AdManager.Instance.ShowInterstitial()` to show interstitial ads, you can pass sucess and fail actions to handle the procedure.
    - Call `Admanager.Instance.ShowRewarded()` to show rewarded ads. you should pass sucess and fail actions to handle the procedure.
    - Call `AdManager.Instance.ShowBanner()` to show banner.

## Ad Services

### [Admob](https://developers.google.com/admob/unity/quick-start)
This comes with **ExternalDependencyManager** and **Plugins** folders to resolve dependencies. Also GoogleMobileAds and Paycheque/SDKs/Admob contains Admob SDK and configuration files.

Paycheque/Admob/AdmobAdService handles the logic for admob. Ad Manager prefab uses Admob test ids for unit ids.

> Make sure to fill Google Mobile Ads App ID in GoogleMobileAds/Resources/GoogleMobileAdsSettings.asset or Assets > Google Mobile Ads > Settings.

### [Unity Ads](https://docs.unity3d.com/Packages/com.unity.ads@3.7/manual/index.html)
This uses Unity's built-in Advertisement package. The package will be installed by going to Services tab and enable ads.

> Note that the App ID should be pasted into Game ID field in Paycheque/UnityAd/UnityAdService.

## Options

### Placement exclusion
Specific placements for services can be ignored in show ad procedure. For example by exluding interstitial ads for Unity Ad Service, you make sure that no interstitial ad comes from Unity Ads.

### Rewarded Ad Button
You can add this component to a button and change the look/behaviour of the button when the rewarded ad is available. For example you can make the button interactable or remove loading spinner to indicate that the ad is available.