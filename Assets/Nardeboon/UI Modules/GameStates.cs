using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;


[Serializable]
public enum GameStates {
    Freeze,
    Splash,
    MainMenu,
    Gameplay,
    Win,
    Lose,
}

[Serializable]
public class UIMaps : ReflectableClass {
    public UIElement Splash;
    public UIElement MainMenu;
    public UIElement Gameplay;
    public UIElement Win;
    public UIElement Lose;
}

public abstract class ReflectableClass
{
    public object this[string propertyName]
    {
        get
        {
            Type classType = GetType();
            FieldInfo fieldInfo = classType.GetField(propertyName);
            return fieldInfo.GetValue(this);
        }
        set
        {
            Type classType = GetType();
            FieldInfo fieldInfo = classType.GetField(propertyName);
            fieldInfo.SetValue(this, value);
        }

    }
}

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

    // TODO: This should be bound to analytics events like onLevelStart
    public static Action<GameStates> onStateChange;
}

public static class UIEvents {
    public static Action<bool> onVibrationSetEvent;
}

public static class InventoryEvents {
    public static Action onCoinUpdate;
}