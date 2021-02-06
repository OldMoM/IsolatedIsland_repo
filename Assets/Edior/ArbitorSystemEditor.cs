using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

//[CustomEditor(typeof(ArbitorSystem))]
public class ArbitorSystemEditor : Editor
{
    private ArbitorSystem _arbitor;
    private FacilityData facility;
    private bool facilityDataFolder;

    private void OnEnable()
    {
        //_arbitor = (ArbitorSystem)target;
    }
    public override void OnInspectorGUI()
    {
      
    }
}

