using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Connect resource loader to loading screen

public static class ResourceLoader {
    private static readonly string itemsPath = "Items/";
    public static T LoadData<T>(string path) where T : UnityEngine.Object {
        return Resources.Load<T>(path);
    }

    public static GameItem LoadItem(string name) => LoadData<GameItem>(itemsPath + name);

    // TODO: Load async and use in loading process
    public static GameItem[] LoadAllItems() => Resources.LoadAll<GameItem>(itemsPath);
}