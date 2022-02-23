public static class GameEvents {
    // Progression events
    public static Action<int> onLevelStart;
    public static Action<int> onLevelLose;
    public static Action<int> onLevelWin;

    // Custom events
    public static Action<string, float> onCustomEvent;

    // Resource events
    public static Action<int> onCurrencySpend;
    public static Action<int> onCurrencyEarn;

    // Ads events
    public static Action onAdFail;
    public static Action onAdShow;
}