using UnityEngine.UI;

[System.Serializable]
public class UIElements {
    // TODO: Add all UI Elements here so they can be retrived from outside if needed
    public Button retryButton;
    public Button reviveButton;
    public Button nextLevelButton;
    public Text[] levelTexts;
    public LevelProgressIndicator levelProgressIndicator;
    public ButtonDown startGame;
}