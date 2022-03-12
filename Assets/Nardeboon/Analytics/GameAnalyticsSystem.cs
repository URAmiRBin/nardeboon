using GameAnalyticsSDK;

public class GameAnalyticsSystem : AnalyticsSystem {
    public override void Initialize() {
        base.Initialize();
        GameAnalytics.Initialize();
    }

    protected override void SendLevelStartEvent(int level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level.ToString());
    }
    protected override void SendLevelWinEvent(int level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level.ToString());
    }
    protected override void SendLevelLoseEvent(int level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level.ToString());
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