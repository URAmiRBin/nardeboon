public class AnalyticsSystem {

    public void Initialize() {
        GameEvents.onLevelStart += SendLevelStartEvent;
        GameEvents.onLevelWin += SendLevelWinEvent;
        GameEvents.onLevelLose += SendLevelLoseEvent;
        GameEvents.onCustomEvent += SendCustomEvent;
        GameEvents.onCurrencyEarn += SendCurrencyEarnEvent;
        GameEvents.onCurrencySpend += SendCurrencySpendEvent;
        GameEvents.onAdFail += SendAdFailEvent;
        GameEvents.onAdShow += SendAdShowEvent;
    }

    public void Destroy() {
        GameEvents.onLevelStart -= SendLevelStartEvent;
        GameEvents.onLevelWin -= SendLevelWinEvent;
        GameEvents.onLevelLose -= SendLevelLoseEvent;
        GameEvents.onCustomEvent -= SendCustomEvent;
        GameEvents.onCurrencyEarn -= SendCurrencyEarnEvent;
        GameEvents.onCurrencySpend -= SendCurrencySpendEvent;
        GameEvents.onAdFail -= SendAdFailEvent;
        GameEvents.onAdShow -= SendAdShowEvent;
    }

    void SendLevelStartEvent(int level) {}
    void SendLevelWinEvent(int level) {}
    void SendLevelLoseEvent(int level) {}
    void SendCustomEvent(string type, float value) {}
    void SendCurrencySpendEvent(int value) {}
    void SendCurrencyEarnEvent(int value) {}
    void SendAdFailEvent() {}
    void SendAdShowEvent() {}
}