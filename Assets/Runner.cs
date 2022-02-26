using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour {
    // More analytics APIs might be added later
    [Header("Game Analytics")]
    [SerializeField] bool useAnalytics;
    [SerializeField] GameObject gameAnalytics;
    [SerializeField] string gameAnalyticsGameKey;
    [SerializeField] string gameAnalyticsSecretKey;

    // Dependencies
    AnalyticsSystem analyticsSystem;
    
    void Awake() {
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
            analyticsSystem = new GameAnalyticsSystem();
            analyticsSystem.Initialize();
        }

        //TODO: Do the loading progression here
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
    }

    void OnDestroy() {
        analyticsSystem.Destroy();
    }
}
