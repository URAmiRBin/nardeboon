using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviourSingletion<Runner> {
    [SerializeField] CoreGameManager gameManagerPrefab;
    CoreGameManager _gameManager;

    [SerializeField] AnalyticsConfig analyticsConfig;

    [SerializeField] AdConfig adConfig;
    public static AdManager AdManager;

    [SerializeField] UIConfig uiConfig;
    public static UIManager UIManager;
    public static UIElements UIElements {
        get => UIManager?.Elements;
    }

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
        _gameManager = Instantiate(gameManagerPrefab, transform);
        UIManager = Instantiate(uiConfig.uiManagerPrefab);
        UIManager.Initialize(uiConfig);
        loadingPanel = UIManager.Elements.loadingScreen;

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
            AdManager = new GameObject("AdManager").AddComponent<AdManager>();
            AdManager.BuildServices(adConfig);
            AdManager.InitializeAds(adConfig.isTestBuild);
        } catch (Exception) {
            Debug.LogError("Can not initialize ad services!");
        }

        vibrationManager = new VibrationManager(shortVibrationDurationInMilliseconds, longVibrationDurationInMilliseconds, logVibrationInEditor);        
        
        DontDestroyOnLoad(new GameObject("Inventory").AddComponent<PlayerInventory>());
        _gameManager.Initialize();
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
        _gameManager.StartGame();
    }

    void OnDestroy() {
        Analytics.Destroy();
    }
}
