using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi 
{
    public static class Entity
    {
        public static ReactiveDictionary<int, GameObject> garbages = new ReactiveDictionary<int, GameObject>();

        public static GarbageGeneratorModel garbageGeneratorModel = new GarbageGeneratorModel(false);

        public static ReactiveDictionary<Vector2Int, IslandModel> islandModels = new ReactiveDictionary<Vector2Int, IslandModel>();
        /// <summary>
        /// The environmnt variables
        /// </summary>
        public static Hashtable environVars = new Hashtable()
        {
            
        };
        /// <summary>
        /// The game triggers
        /// </summary>
        public static Dictionary<string, Subject<Unit>> gameTriggers = new Dictionary<string, Subject<Unit>>()
        {
            { "gameFailed"   , new Subject<Unit>()},//游戏失败
            { "onGamePaused" , new Subject<Unit>()},//游戏暂停
            { "onGameResumed", new Subject<Unit>()},//游戏恢复
        };
    }
}
