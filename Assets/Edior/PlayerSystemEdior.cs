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
        private PlayerSystem playerSystem;
        private bool foldMovementModel;
        private void OnEnable()
        {
            playerSystem = (PlayerSystem)target;
        }
        public override void OnInspectorGUI()
        {
            if (playerSystem.Movement == null)
            {
                return;
            }
            GUILayout.Label("-----为了方便校准-----");
            GUILayout.Label("-----以下参数为只读的数值-----");

            foldMovementModel = EditorGUILayout.Foldout(foldMovementModel, "移动状态运行参数");

            if (foldMovementModel)
            {
                EditorGUILayout.FloatField("移动速率:", playerSystem.Movement.moveSpeed);
                EditorGUILayout.Vector3Field("移动速度", playerSystem.Movement.Velocity);
            }

            EditorGUILayout.EnumFlagsField("玩家当前状态", playerSystem.PlayerState);
        }
    }
}
