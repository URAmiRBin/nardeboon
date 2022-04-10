using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour {
    [SerializeField] AnalyticsConfig analyticsConfig;

    [SerializeField] AdConfig adConfig;
    AdManager adManager;

    [SerializeField] UIConfig uiConfig;
    UIManager uiManager;
    ProgressLoadingScreen loadingPanel;

    [Header("Vibration")]
    [SerializeField] bool logVibrationInEditor;
    [SerializeField] long shortVibrationDurationInMilliseconds;
    [SerializeField] long longVibrationDurationInMilliseconds;
    public static VibrationManager vibrationManager;

    
    
    // Dependencies
    public static AnalyticsSystem Analytics {get; private set;}
    
    void Awake() {
        DontDestroyOnLoad(this);
        ConfigPreprocess();
        SetupServices();
        StartCoroutine(LoadGameScene());
    }

    void ConfigPreprocess() {
        // TODO: Remove parts according to used packages
        uiConfig.agreementsText = uiConfig.agreementsText.Replace("****", Application.productName);
    }

    void SetupServices() {
        uiManager = Instantiate(uiConfig.uiManagerPrefab);
        uiManager.Initialize(uiConfig);
        loadingPanel = uiManager.Elements.loadingScreen;

        if (analyticsConfig.useAnalytics) {
            try {
                // Set GA settings
                GameAnalyticsSDK.Setup.Settings gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

                // TODO: Multiplatform support
                if (gaSettings.Platforms.Count == 0) {
                    gaSettings.InfoLogEditor = false;
                    gaSettings.AddPlatform(RuntimePlatform.Android);
                    gaSettings.UpdateGameKey(0, analyticsConfig.gameAnalyticsGameKey);
                    gaSettings.UpdateSecretKey(0, analyticsConfig.gameAnalyticsSecretKey);
                    gaSettings.Build[0] = Application.version;    
                }

                // Initialize GA
                Instantiate(analyticsConfig.gameAnalytics);
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
