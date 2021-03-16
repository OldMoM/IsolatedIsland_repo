using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadPrefab 
{
    public static GameObject GetUIPrefab(string prefab)
    {
        var prefabObject = Resources.Load("Prefabs/UIComponents/" + prefab) as GameObject;
        if (prefabObject == null)
        {
            throw new System.Exception("Fail to find " + prefab);
        }
        return prefabObject;
    }
    public static GameObject GetSystemPrefab(string prefabName)
    {
        var systemPrefab = Resources.Load("Prefabs/Systems/" + prefabName) as GameObject;
        if (systemPrefab is null)
        {
            throw new System.Exception("Failed to find " + prefabName);
        }
        return systemPrefab;
    }
}
