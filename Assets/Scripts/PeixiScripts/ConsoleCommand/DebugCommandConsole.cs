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

            #region//BuildSystem Commands
            DebugLogConsole.AddCommand<Vector2Int>("buildIslandAt", "在网格坐标[x,y]处增加Island", BuildSystemCommands.BuildIslandAt);
            DebugLogConsole.AddCommand<Vector2Int>("removeIslandAt", "移除网格坐标[x,y]处的Island", BuildSystemCommands.RemoveIslandAt);
            DebugLogConsole.AddCommand<Vector2Int, string>("buildFacility", "在网格上建造设施", BuildSystemCommands.BuildFacilityAt);
            DebugLogConsole.AddCommand<Vector2Int>("buildislandsby", "建造尺寸为x,y的Island集群", BuildSystemCommands.BuildIslandsBy);
            DebugLogConsole.AddCommand<Vector2Int, int>("setIslandDurabilityTo", "将[x,y]处的Island的耐久度设置为", BuildSystemCommands.SetIslandDurability);
            #endregion
        }
    }
}
