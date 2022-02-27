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

    [Header("UI")]
    [SerializeField] LoadingScreen loadingPanel;

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
    }

    IEnumerator LoadGameScene() {
        AsyncOperation gameLoadOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);

        while (!gameLoadOperation.isDone) {
            loadingPanel.SetProgress(gameLoadOperation.progress);
            yield return null;
        }
        loadingPanel.SetProgress(gameLoadOperation.progress);
    }

    void OnDestroy() {
        Analytics.Destroy();
    }
}
