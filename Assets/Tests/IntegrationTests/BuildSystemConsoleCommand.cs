using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using Peixi;
using UnityEngine.Assertions;


public class BuildSystemConsoleCommand : MonoBehaviour
{
    // Start is called before the first frame update

    public IBuildSystem ibuildSystem => InterfaceArichives.Archive.IBuildSystem;
    void Start()
    {
        DebugLogConsole.AddCommand<Vector2Int>("buildIslandAt", "在网格坐标[x,y]处增加Island", BuildIslandAt);
        DebugLogConsole.AddCommand<Vector2Int>("removeIslandAt", "移除网格坐标[x,y]处的Island", RemoveIslandAt);
        DebugLogConsole.AddCommand<Vector2Int,string>("buildFacility", "在网格上建造设施", BuildFacilityAt);
        DebugLogConsole.AddCommand<Vector2Int>("buildislandsby", "建造尺寸为x,y的Island集群", BuildIslandsBy);
        DebugLogConsole.AddCommand<Vector2Int, int>("setIslandDurabilityTo", "将[x,y]处的Island的耐久度设置为", SetIslandDurability);
    }
    public static void BuildIslandAt(Vector2Int gridPos)
    {
        IBuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        buildSystem.BuildIslandAt(gridPos);
    }
    public static void RemoveIslandAt(Vector2Int gridPos)
    {
        IBuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        buildSystem.RemoveIslandAt(gridPos);
    }
    public static void BuildFacilityAt(Vector2Int gridPos,string type)
    {
        IBuildSystem buildSystem = FindObjectOfType<BuildSystem>();
        Assert.IsNotNull(buildSystem, "BuildSystem.prefab is not configurated in Hirerarchy");
        buildSystem.BuildFacility(gridPos, type);
    }
    public static void BuildIslandsBy(Vector2Int size)
    {
        IBuildSystem buildSystem = FindObjectOfType<BuildSystem>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                buildSystem.BuildIslandAt(new Vector2Int(i, j));
            }
        }
    }
    public static void SetIslandDurability(Vector2Int islandGridPos,int durability)
    {
        IBuildSystem ibuildSystem = FindObjectOfType<BuildSystem>();
        var iisland = ibuildSystem.GetIslandInterface(islandGridPos);
        iisland.SetDurabilityTo(durability);
    }
}
