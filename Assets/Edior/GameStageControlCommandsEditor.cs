using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using UnityEditor;

[CustomEditor(typeof(GameStageControlCommands))]
public class GameStageControlCommandsEditor : Editor
{
    GameStageControlCommands gameStage;
    private void OnEnable()
    {
        gameStage = (GameStageControlCommands)target;
    }

    public override void OnInspectorGUI()
    {
        
    }

}
