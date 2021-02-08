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
        private bool foldProperty;

        private void OnEnable()
        {
            playerSystem = (PlayerSystem)target;
        }
        public override void OnInspectorGUI()
        {
            GUILayout.Label("-----为了方便校准-----");
            GUILayout.Label("-----以下参数为只读的数值-----");
            foldProperty = EditorGUILayout.Foldout(foldProperty, "玩家属性");
            if (foldProperty)
            {
                EditorGUILayout.IntField("健康值", playerSystem.PlayerPropertySystem.Health);
                EditorGUILayout.IntField("饥饿值", playerSystem.PlayerPropertySystem.Hunger);
                EditorGUILayout.IntField("心情值", playerSystem.PlayerPropertySystem.Pleasure);
                EditorGUILayout.IntField("口渴值", playerSystem.PlayerPropertySystem.Thirst);
            }


            if (playerSystem.Movement == null)
            {
                return;
            }

            foldMovementModel = EditorGUILayout.Foldout(foldMovementModel, "移动状态运行参数");

            if (foldMovementModel)
            {
                EditorGUILayout.FloatField("移动速率:", playerSystem.Movement.moveSpeed);
                EditorGUILayout.Vector3Field("移动速度", playerSystem.Movement.Velocity);
            }
            EditorGUILayout.EnumFlagsField("玩家当前状态", playerSystem.StateController.playerState.Value);

        }
    }
}
