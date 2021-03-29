using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi.Commands
{
    public static class GameStageCommands 
    {
        public static void SetGameStage(string stageId)
        {
            GameStageManager.StartStage(stageId);
        }
    }
}
