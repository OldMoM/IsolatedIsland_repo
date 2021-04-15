using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using Peixi;



namespace Peixi.Commands
{
    public class DebugCommandConsole : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DebugLogConsole.AddCommand<string,int>("inventory.additem", "向背包中添加物品",InventoryCommands.AddItem);

            DebugLogConsole.AddCommand<string, Vector2Int>("androidra.buildAt", "命令虫丸进行建造", AndroidraCommands.BuildAt);
            DebugLogConsole.AddCommand<Vector2Int>("androidra.restoreIsland", "命令虫丸修复岛块", AndroidraCommands.RestoreIsland);

            DebugLogConsole.AddCommand<string>("setStage", "设定剧情节点", GameStageCommands.SetGameStage);
        }
    }
}
