using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

namespace GameEditors
{
    [CustomEditor(typeof(AndroidraSystem))]
    public class AndroidraEditor : Editor
    {
        private AndroidraSystem system;

        private void OnEnable()
        {
            system = target as AndroidraSystem;
        }

        public override void OnInspectorGUI()
        {

            if (system.androidState != null)
            {
                EditorGUILayout.EnumFlagsField("Androidra's State", system.androidState.State);
            }

            if (system.navPresenter != null)
            {
                system.navPresenter.Target = EditorGUILayout.Vector3Field("Andtroidra's Target", system.navPresenter.Target);
            }
        }
    }
}
