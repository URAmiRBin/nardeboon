public class GameAnalyticsSystem : AnalyticsSystem {
    void SendLevelStartEvent(int level) {}
    void SendLevelWinEvent(int level) {}
    void SendLevelLoseEvent(int level) {}
    void SendCustomEvent(string type, float value) {}
    void SendCurrencySpendEvent(int value) {}
    void SendCurrencyEarnEvent(int value) {}
    void SendAdFailEvent() {}
    void SendAdShowEvent() {}
}