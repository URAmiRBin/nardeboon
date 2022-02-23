using GameAnalyticsSDK;

public class GameAnalyticsSystem : AnalyticsSystem {
    public override void Initialize() {
        base.Initialize();
        GameAnalytics.Initialize();
    }

    protected override void SendLevelStartEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level);
    }
    protected override void SendLevelWinEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level);
    }
    protected override void SendLevelLoseEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level);
    }
    protected override void SendCustomEvent(string eventName, float value) {
        GameAnalytics.NewDesignEvent(eventName, value);
    }
    protected override void SendCurrencySpendEvent(int value) {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "", value, "", "");
    }
    protected override void SendCurrencyEarnEvent(int value) {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "", value, "", "");
    }
    protected override void SendAdFailEvent() {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Undefined, "", "", GAAdError.Unknown);
    }
    protected override void SendAdShowEvent() {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Undefined, "", "", GAAdError.Unknown);
    }
}