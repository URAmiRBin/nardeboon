using System;
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
    [SerializeField] AdConfig adConfig;
    AdManager adManager;

    [Header("Vibration")]
    [SerializeField] bool logVibrationInEditor;
    [SerializeField] long shortVibrationDurationInMilliseconds;
    [SerializeField] long longVibrationDurationInMilliseconds;
    public static VibrationManager vibrationManager;

    [Header("UI Config")]
    [SerializeField] UIConfig uiConfig;

    UIManager uiManager;
    ProgressLoadingScreen loadingPanel;
    
    // Dependencies
    public static AnalyticsSystem Analytics {get; private set;}
    
    void Awake() {
        DontDestroyOnLoad(this);
        SetupServices();
        StartCoroutine(LoadGameScene());
    }

    void SetupServices() {
        uiManager = Instantiate(uiConfig.uiManagerPrefab);
        uiManager.Initialize(uiConfig);
        loadingPanel = uiManager.Elements.loadingScreen;

        if (useAnalytics) {
            try {
                // Set GA settings
                GameAnalyticsSDK.Setup.Settings gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

                // TODO: Multiplatform support
                if (gaSettings.Platforms.Count == 0) {
                    gaSettings.InfoLogEditor = false;
                    gaSettings.AddPlatform(RuntimePlatform.Android);
                    gaSettings.UpdateGameKey(0, gameAnalyticsGameKey);
                    gaSettings.UpdateSecretKey(0, gameAnalyticsSecretKey);
                    gaSettings.Build[0] = Application.version;    
                }

                // Initialize GA
                Instantiate(gameAnalytics);
                Analytics = new GameAnalyticsSystem();
                Analytics.Initialize();
            } catch (Exception) {
                Debug.LogError("Can not initialize Analytics!");
            }
        }

        try {
            adManager = new GameObject("AdManager").AddComponent<AdManager>();
            adManager.BuildServices(adConfig);
            adManager.InitializeAds(adConfig.isTestBuild);
        } catch (Exception) {
            Debug.LogError("Can not initialize ad services!");
        }

        vibrationManager = new VibrationManager(shortVibrationDurationInMilliseconds, longVibrationDurationInMilliseconds, logVibrationInEditor);        
    }

    IEnumerator LoadGameScene() {
        if (SceneManager.sceneCountInBuildSettings <= 1) {
            Debug.LogError("Core game scene is not added to the build settings!");
            yield break;
        }
        AsyncOperation gameLoadOperation = SceneManager.LoadSceneAsync(1);
        
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
