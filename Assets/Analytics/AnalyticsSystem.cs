public abstract class AnalyticsSystem {

    public virtual void Initialize() {
        GameEvents.onLevelStart += SendLevelStartEvent;
        GameEvents.onLevelWin += SendLevelWinEvent;
        GameEvents.onLevelLose += SendLevelLoseEvent;
        GameEvents.onCustomEvent += SendCustomEvent;
        GameEvents.onCurrencyEarn += SendCurrencyEarnEvent;
        GameEvents.onCurrencySpend += SendCurrencySpendEvent;
        GameEvents.onAdFail += SendAdFailEvent;
        GameEvents.onAdShow += SendAdShowEvent;
    }

    public virtual void Destroy() {
        GameEvents.onLevelStart -= SendLevelStartEvent;
        GameEvents.onLevelWin -= SendLevelWinEvent;
        GameEvents.onLevelLose -= SendLevelLoseEvent;
        GameEvents.onCustomEvent -= SendCustomEvent;
        GameEvents.onCurrencyEarn -= SendCurrencyEarnEvent;
        GameEvents.onCurrencySpend -= SendCurrencySpendEvent;
        GameEvents.onAdFail -= SendAdFailEvent;
        GameEvents.onAdShow -= SendAdShowEvent;
    }

    protected abstract void SendLevelStartEvent(string level);
    protected abstract void SendLevelWinEvent(string level);
    protected abstract void SendLevelLoseEvent(string level);
    protected abstract void SendCustomEvent(string type, float value);
    protected abstract void SendCurrencySpendEvent(int value);
    protected abstract void SendCurrencyEarnEvent(int value);
    protected abstract void SendAdFailEvent();
    protected abstract void SendAdShowEvent();
}