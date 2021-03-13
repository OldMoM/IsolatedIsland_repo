using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

[CustomEditor(typeof(ArbitorSystem))]
public class ArbitorSystemEditor : Editor
{
    private ArbitorSystem _arbitor;
    private FacilityData facility;
    private bool facilityDataFolder;
    private bool IslandInteractAgent;

    private void OnEnable()
    {
        _arbitor = (ArbitorSystem)target;
    }
    public override void OnInspectorGUI()
    {

        IslandInteractAgent = EditorGUILayout.Foldout(IslandInteractAgent, "岛块互动运行参数");

        if (_arbitor.IslandInteractAgent != null && IslandInteractAgent)
        {
            EditorGUILayout.Vector2IntField("玩家网格坐标", _arbitor.IslandInteractAgent.ContactBrokenIsland);
            EditorGUILayout.TextField("玩家同损坏岛块接触", _arbitor.IslandInteractAgent.IsContactBrokenIsland.ToString());
        }
        
    }
}

