using System;

public static class GameEvents {
    // Progression events
    public static Action<string> onLevelStart;
    public static Action<string> onLevelLose;
    public static Action<string> onLevelWin;

    // Custom events
    public static Action<string, float> onCustomEvent;

    // Resource events
    public static Action<int> onCurrencySpend;
    public static Action<int> onCurrencyEarn;

    // Ads events
    public static Action onAdFail;
    public static Action onAdShow;
}