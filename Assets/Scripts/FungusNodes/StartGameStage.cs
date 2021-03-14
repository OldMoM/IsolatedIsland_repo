using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;

namespace Fungus
{
    [CommandInfo("GameStage",
                 "StartStage",
                 "开始游戏事件节点")]
    public class StartGameStage :Command
    {
        public string stageId;
        public override void OnEnter()
        {
            GameStageManager.StartStage(stageId);
            Continue();
        }
    }
}
