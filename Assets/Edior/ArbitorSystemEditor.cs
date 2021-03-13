using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

[CustomEditor(typeof(ArbitorSystem))]
public class ArbitorSystemEditor : Editor
{
    private ArbitorSystem arbitor;
    private FacilityData facility;
    private bool facilityDataFolder;
    private bool IslandInteractAgent;

    private void OnEnable()
    {
        arbitor = (ArbitorSystem)target;
    }
    public override void OnInspectorGUI()
    {
        if (arbitor.facilityInteractAgent != null)
        {
            EditorGUILayout.LabelField("和玩家接触的互动物体:");
            arbitor.facilityInteractAgent.ContactingItems
                .ForEach(x =>
                {
                    EditorGUILayout.LabelField("Name: " + x.name + " Hashcode: " + x.instanceId);
                });
        }
        
        
    }
}

