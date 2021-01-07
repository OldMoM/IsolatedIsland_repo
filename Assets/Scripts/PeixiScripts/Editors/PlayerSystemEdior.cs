using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Peixi;

namespace GameEditors
{
    [CustomEditor(typeof(PlayerSystem))]
    public class PlayerSystemEdior : Editor
    {
        private PlayerSystem m_playerSystem;
        private bool foldMovementModel;
        private void OnEnable()
        {
            m_playerSystem = (PlayerSystem)target;
        }
        public override void OnInspectorGUI()
        {
            if (m_playerSystem.Movement == null)
            {
                return;
            }
            GUILayout.Label("-----为了方便校准-----");
            GUILayout.Label("-----以下参数为只读的数值-----");

            foldMovementModel = EditorGUILayout.Foldout(foldMovementModel, "Movement model");

            if (foldMovementModel)
            {
                EditorGUILayout.FloatField("移动速率:", m_playerSystem.Movement.moveSpeed);
                EditorGUILayout.Vector3Field("移动速度", m_playerSystem.Movement.Velocity);
            }
        }
    }
}
