using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] AdConfig adConfig;
    AdManager adManager;
    

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
            Instantiate(gameAnalytics);
            Analytics = new GameAnalyticsSystem();
            Analytics.Initialize();
        }

        adManager = new GameObject("AdManager").AddComponent<AdManager>();
        adManager.BuildServices(adConfig);
        adManager.InitializeAds(adConfig.isTestBuild);
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