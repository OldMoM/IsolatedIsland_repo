using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadPrefab 
{
   public static GameObject GetUIComponents(string prefab)
    {
        var prefabObject = Resources.Load("Prefabs/UIComponents/" + prefab) as GameObject;
        if (prefabObject == null)
        {
            throw new System.Exception("Fail to find " + prefab);
        }
        return prefabObject;
    }
}
