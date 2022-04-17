using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.adjust.sdk;

public class Runner : MonoBehaviour {
    [SerializeField] bool isProductionBuild;
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

    [Header("Economy")]
    [SerializeField] GameItem mainCurrency;
    public static Inventory InventorySystem;

    [Header("Save")]
    [SerializeField] GameObject easySaveManagerPrefab;

    ProgressLoadingScreen loadingPanel;

    [Header("Vibration")]
    [SerializeField] bool logVibrationInEditor;
    [SerializeField] long shortVibrationDurationInMilliseconds;
    [SerializeField] long longVibrationDurationInMilliseconds;
    public static VibrationManager vibrationManager;

    // Dependencies
    public static AnalyticsSystem GameAnalytics {get; private set;}
    public static AnalyticsSystem AdjustAnalytics {get; private set;}
    
    void Awake() {
        // FIXME: Start the loading process before setting up services not after it
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
        // FIXME: This function is getting looong
        _gameManager = Instantiate(gameManagerPrefab);
        DontDestroyOnLoad(_gameManager);

        InventorySystem = new GameObject("Inventory System").AddComponent<Inventory>();
        DontDestroyOnLoad(InventorySystem);
        InventorySystem.Initialize(mainCurrency);
        
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

                if (gaSettings.ResourceCurrencies.Count == 0) gaSettings.ResourceCurrencies.Add(Inventory.Instance.MainCurrency);
                if (gaSettings.ResourceItemTypes.Count == 0) gaSettings.ResourceItemTypes.Add("Game Item");

                // Initialize GA
                Instantiate(analyticsConfig.gameAnalytics);
                GameAnalytics = new GameAnalyticsSystem();
                GameAnalytics.Initialize();
            } catch (Exception) {
                Debug.LogError("Can not initialize GameAnalytics!");
            }

            try {
                if (analyticsConfig.adjustPrefab != null) {
                    DontDestroyOnLoad(Instantiate(analyticsConfig.adjustPrefab));
                    AdjustAnalytics = new AdjustAnalyticsSystem();
                    AdjustEnvironment adjustEnv = isProductionBuild ? AdjustEnvironment.Production : AdjustEnvironment.Sandbox;
                    AdjustConfig adjustConfig = new AdjustConfig(analyticsConfig.adjustToken, adjustEnv);
                    adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
                    Adjust.start(adjustConfig);
                    AdjustAnalytics.Initialize();
                }
            } catch (Exception) {
                Debug.LogError("Can not initialize Adjust!");
            }
        }

        try {
            adConfig.isTestBuild = !isProductionBuild;
            AdManager = new GameObject("AdManager").AddComponent<AdManager>();
            AdManager.BuildServices(adConfig);
            AdManager.InitializeAds(adConfig.isTestBuild);
        } catch (Exception) {
            Debug.LogError("Can not initialize ad services!");
        }

        vibrationManager = new VibrationManager(shortVibrationDurationInMilliseconds, longVibrationDurationInMilliseconds, logVibrationInEditor);        
        
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
        GameAnalytics?.Destroy();
        AdjustAnalytics?.Destroy();
    }
}
