using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;


[Serializable]
public enum GameStates {
    Empty,
    Splash,
    MainMenu,
    Win,
    Lose,
}

[Serializable]
public class UIMaps : ReflectableClass {
    public UIElement Splash;
    public UIElement MainMenu;
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