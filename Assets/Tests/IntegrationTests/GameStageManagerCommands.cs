using System.Collections;
using UnityEngine;
using IngameDebugConsole;
using Peixi;
using UniRx;

public class GameStageManagerCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DebugLogConsole.AddCommand<string>("set.stage", "", (string stageId) =>
        {
            GameStageManager.StartStage(stageId);
        });
    }
}
