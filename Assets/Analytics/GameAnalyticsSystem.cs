using GameAnalyticsSDK;

public class GameAnalyticsSystem : AnalyticsSystem {
    void SendLevelStartEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level);
    }
    void SendLevelWinEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level);
    }
    void SendLevelLoseEvent(string level) {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level);
    }
    void SendCustomEvent(string eventName, float value) {
        GameAnalytics.NewDesignEvent(eventName, value);
    }
    void SendCurrencySpendEvent(int value) {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "", value, "", "");
    }
    void SendCurrencyEarnEvent(int value) {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "", value, "", "");
    }
    void SendAdFailEvent() {
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Undefined, "", "", GAAdError.Unknown);
    }
    void SendAdShowEvent() {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Undefined, "", "", GAAdError.Unknown);
    }
}