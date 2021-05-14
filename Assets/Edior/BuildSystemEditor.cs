using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using UnityEditor;

namespace GameEditors
{
    [CustomEditor(typeof(BuildSystem))]
    public class BuildSystemEditor : Editor
    {
        private BuildSystem _system;
        private bool foldSettings;
        private void OnEnable()
        {
            _system = (BuildSystem)target;
        }
        public override void OnInspectorGUI()
        {

            foldSettings = EditorGUILayout.Foldout(foldSettings, "Grid Settings");
            if (foldSettings)
            {
                EditorGUILayout.FloatField("Cell size", _system.settings._cellSize);
                EditorGUILayout.Vector2Field("Grid origin", _system.settings._origin);

            }
        }                    
    }
}
