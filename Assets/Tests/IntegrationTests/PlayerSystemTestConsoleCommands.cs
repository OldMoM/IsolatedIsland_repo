using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using Peixi;
public class PlayerSystemTestConsoleCommands : MonoBehaviour
{
    // Start is called before the first frame update
    IPlayerPropertySystem property;
    void Start()
    {
        property = InterfaceArichives.Archive.IPlayerPropertySystem;
        DebugLogConsole.AddCommand<int>("changeplayer.health", "增减玩家的健康值", (change) =>
        {
            property.ChangeHealth(change);
        });
        DebugLogConsole.AddCommand<int>("changeplayer.hunger", "增减玩家的饥饿值", (change) =>
        {
            property.ChangeHunger(change);
        });
        DebugLogConsole.AddCommand<int>("changeplayer.thirst", "增减玩家的口渴值", (change) =>
        {
            property.ChangeThirst(change);
        });
        DebugLogConsole.AddCommand<int>("changeplayer.pleasure", "增减玩家的心情值", (change) =>
        {
            property.ChangePleasure(change);
        });
    }
}
