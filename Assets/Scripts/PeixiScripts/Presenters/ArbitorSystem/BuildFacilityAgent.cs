using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public static class BuildFacilityAgent 
    {
        /// <summary>
        /// Gets the plastic in inventory.
        /// </summary>
        /// <value>
        /// The plastic in inventory.
        /// </value>
        private static int plasticInInventory => InterfaceArichives.Archive.IInventorySystem.GetAmount(ItemTags.plastic);
        /// <summary>
        /// Gets the fiber in inventory.
        /// </summary>
        /// <value>
        /// The fiber in inventory.
        /// </value>
        private static int fiberInInventory => InterfaceArichives.Archive.IInventorySystem.GetAmount(ItemTags.fiber);
        /// <summary>
        /// Satisfies the build condition ?
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <returns></returns>
        private static bool SatisfyBuildCondition(string facilityName)
        {
            bool enoughPlastic = false;
            bool enoughFiber = false;

            if (facilityName == PrefabTags.fishPoint)
            {
                var plasticCost = GameConfig.Singleton.InteractionConfig["buildFishPoint_plasticCost"];
                var fiberCost = GameConfig.Singleton.InteractionConfig["buildFishPoint_fiberCost"];

                enoughPlastic = plasticInInventory >= plasticCost;
                enoughFiber = fiberInInventory >= fiberCost;
            }
            if (facilityName == PrefabTags.foodPlant)
            {
                var plasticCost = GameConfig.Singleton.InteractionConfig["buildFoodPlant_plasticCost"];
                var fiberCost = GameConfig.Singleton.InteractionConfig["buildFoodPlant_fiberCost"];

                enoughPlastic = plasticInInventory >= plasticCost;
                enoughFiber = fiberInInventory >= fiberCost;
            }
            if (facilityName == PrefabTags.waterPuifier)
            {
                var plasticCost = GameConfig.Singleton.InteractionConfig["buildWaterPuifier_plasticCost"];
                var fiberCost = GameConfig.Singleton.InteractionConfig["buildWaterPuifier_fiberCost"];

                enoughPlastic = plasticInInventory >= plasticCost;
                enoughFiber = fiberInInventory >= fiberCost;
            }
            if (facilityName == PrefabTags.garbageCollector)
            {
                var plasticCost = GameConfig.Singleton.InteractionConfig["buildGarbageCollect_plasticCost"];
                var fiberCost = GameConfig.Singleton.InteractionConfig["buildGarbageCollect_fiberCost"];

                enoughPlastic = plasticInInventory >= plasticCost;
                enoughFiber = fiberInInventory >= fiberCost;
            }

            return enoughFiber && enoughFiber;
        }
        /// <summary>
        /// Gets the build system.
        /// </summary>
        /// <value>
        /// The build system.
        /// </value>
        private static IBuildSystem buildSystem => InterfaceArichives.Archive.IBuildSystem;
        /// <summary>
        /// Gets the inventory system.
        /// </summary>
        /// <value>
        /// The inventory system.
        /// </value>
        private static IInventorySystem inventorySystem => InterfaceArichives.Archive.IInventorySystem;
        /// <summary>
        /// Starts the chat bubble.
        /// </summary>
        /// <param name="chat">The chat.</param>
        private static void StartChatBubble(string[] chat)
        {
            var chatBubble = InterfaceArichives.Archive.InGameUIComponentsManager.ChatBubble;
            var playerPos = InterfaceArichives.Archive.PlayerSystem.OnPlayerPositionChanged;

            chatBubble.StartChat(chat, playerPos);
        }
        /// <summary>
        /// Initializes the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        public static void Init(Hashtable table)
        {
            var onMousePositionChanged = table["onMousePositionChanged"] as IObservable<Vector3>;
            var onMouseClicked = table["onMouseClicked"] as IObservable<(string, Vector3)>;

            OnMouseClicked(onMouseClicked);
        }
        public static void OnMouseClicked(IObservable<(string, Vector3)> onMouseClicked)
        {
            //判断是否有足够材料建造FishPoint
            onMouseClicked
                .Where(x => x.Item1 == PrefabTags.fishPoint)
                .Subscribe(x =>
                {
                    var hasEnoughMat = SatisfyBuildCondition(x.Item1);
                    if (hasEnoughMat)
                    {
                        var gridPos = buildSystem.newWorldToGridPosition(x.Item2);
                        buildSystem.BuildFacility(gridPos, x.Item1);

                        var plasticCost = GameConfig.Singleton.InteractionConfig["buildFishPoint_plasticCost"];
                        var fiberCost = GameConfig.Singleton.InteractionConfig["buildFishPoint_fiberCost"];
                        var plasticCost_int = Convert.ToInt32(plasticCost);
                        var fiberCost_int = Convert.ToInt32(plasticCost);

                        inventorySystem.RemoveItem(ItemTags.plastic, plasticCost_int);
                        inventorySystem.RemoveItem(ItemTags.fiber, fiberCost_int);

                    }
                    else
                    {
                        StartChatBubble(new string[1] { "I don't have enough Material" });
                    }
                });

            //判断材料足够建造foodPlant?
            onMouseClicked
                .Where(x => x.Item1 == PrefabTags.foodPlant)
                .Subscribe(x =>
                {
                    var hasEnoughMat = SatisfyBuildCondition(x.Item1);
                    if (hasEnoughMat)
                    {
                        var gridPos = buildSystem.newWorldToGridPosition(x.Item2);
                        buildSystem.BuildFacility(gridPos, x.Item1);

                        var plasticCost = GameConfig.Singleton.InteractionConfig["buildFoodPlant_plasticCost"];
                        var fiberCost = GameConfig.Singleton.InteractionConfig["buildFoodPlant_fiberCost"];
                        var plasticCost_int = Convert.ToInt32(plasticCost);
                        var fiberCost_int = Convert.ToInt32(plasticCost);

                        inventorySystem.RemoveItem(ItemTags.plastic, plasticCost_int);
                        inventorySystem.RemoveItem(ItemTags.fiber, fiberCost_int);

                    }
                    else
                    {
                        StartChatBubble(new string[1] { "I don't have enough Material" });
                    }
                });

            //判断材料足够建造waterPuifier?
            onMouseClicked
               .Where(x => x.Item1 == PrefabTags.foodPlant)
               .Subscribe(x =>
               {
                   var hasEnoughMat = SatisfyBuildCondition(x.Item1);
                   if (hasEnoughMat)
                   {
                       var gridPos = buildSystem.newWorldToGridPosition(x.Item2);
                       buildSystem.BuildFacility(gridPos, x.Item1);

                       var plasticCost = GameConfig.Singleton.InteractionConfig["buildWaterPuifier_plasticCost"];
                       var fiberCost = GameConfig.Singleton.InteractionConfig["buildWaterPuifier_fiberCost"];
                       var plasticCost_int = Convert.ToInt32(plasticCost);
                       var fiberCost_int = Convert.ToInt32(plasticCost);

                       inventorySystem.RemoveItem(ItemTags.plastic, plasticCost_int);
                       inventorySystem.RemoveItem(ItemTags.fiber, fiberCost_int);

                   }
                   else
                   {
                       StartChatBubble(new string[1] { "I don't have enough Material" });
                   }
               });

            //判断材料足够建造garbageCollector?
            onMouseClicked
               .Where(x => x.Item1 == PrefabTags.foodPlant)
               .Subscribe(x =>
               {
                   var hasEnoughMat = SatisfyBuildCondition(x.Item1);
                   if (hasEnoughMat)
                   {
                       var gridPos = buildSystem.newWorldToGridPosition(x.Item2);
                       buildSystem.BuildFacility(gridPos, x.Item1);

                       var plasticCost = GameConfig.Singleton.InteractionConfig["buildGarbageCollect_plasticCost"];
                       var fiberCost = GameConfig.Singleton.InteractionConfig["buildGarbageCollect_fiberCost"];
                       var plasticCost_int = Convert.ToInt32(plasticCost);
                       var fiberCost_int = Convert.ToInt32(plasticCost);

                       inventorySystem.RemoveItem(ItemTags.plastic, plasticCost_int);
                       inventorySystem.RemoveItem(ItemTags.fiber, fiberCost_int);

                   }
                   else
                   {
                       StartChatBubble(new string[1] { "I don't have enough Material" });
                   }
               });
        }
    }
}
