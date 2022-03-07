using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour {
    // More analytics APIs might be added later
    [Header("Game Analytics")]
    [SerializeField] bool useAnalytics;
    [SerializeField] GameObject gameAnalytics;
    [SerializeField] string gameAnalyticsGameKey;
    [SerializeField] string gameAnalyticsSecretKey;

    [Header("Advertisement")]
    [SerializeField] bool useAdmob;
    [SerializeField] bool isTestBuild;
    [SerializeField] string bannerUnit, interstitialUnit, RewardedUnit;
    

    [Header("Loading Screen")]
    [SerializeField] ProgressLoadingScreen loadingPanel;

    // Dependencies
    public static AnalyticsSystem Analytics {get; private set;}
    
    void Awake() {
        DontDestroyOnLoad(this);
        SetupServices();
        StartCoroutine(LoadGameScene());
    }

    void SetupServices() {
        if (useAnalytics) {
            // Set GA settings
            GameAnalyticsSDK.Setup.Settings gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

            // TODO: Multiplatform support
            gaSettings.AddPlatform(RuntimePlatform.Android);
            gaSettings.UpdateGameKey(0, gameAnalyticsGameKey);
            gaSettings.UpdateSecretKey(0, gameAnalyticsSecretKey);
            gaSettings.Build[0] = Application.version;
            
            // Initialize GA
            Instantiate(gameAnalytics, transform);
            Analytics = new GameAnalyticsSystem();
            Analytics.Initialize();
        }

        var adManager = new GameObject("AdManager", typeof(AdManager));
        adManager.transform.parent = transform;
        if (useAdmob) {
            var admobGameObject = Instantiate(new GameObject("Admob"), adManager.transform);
            var admobAdService = admobGameObject.AddComponent<AdmobAdService>();
            admobAdService.SetUnitIds(bannerUnit, interstitialUnit, RewardedUnit);
            AdManager.Instance.InitializeAds(new AdService[] {admobAdService}, isTestBuild);
        }
    }

    IEnumerator LoadGameScene() {
        AsyncOperation gameLoadOperation = SceneManager.LoadSceneAsync("Main");
        
        loadingPanel.StartProgress();
        while (!gameLoadOperation.isDone) {
            loadingPanel.SetProgress(gameLoadOperation.progress);
            yield return null;
        }
        loadingPanel.FinishProgress();
    }

    void OnDestroy() {
        Analytics.Destroy();
    }
}
