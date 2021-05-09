using Siwei;
using UniRx;
using System;
using UnityEngine;

namespace Peixi
{
    /// <summary>
    /// 处理来自BuildSketch的建造指令
    /// </summary>
    public class BuildIslandAgent 
    {
        private IBuildSystem buildSystem;
        private IInventorySystem inventory;
        private Subject<string> onInteractStart = new Subject<string>();
        private Subject<string> onInteractCompeleted = new Subject<string>();
        private bool isActive;
        private ConcurrentTimer timer = new ConcurrentTimer();

        public IObservable<Vector3> onMouseClicked;
        public IObservable<Unit> OnInteractStart => timer.OnTimerStart;
        public IObservable<Unit> OnInteractCompleted => timer.OnTimerEnd;
        public bool Enough
        {
            get
            {
                var plasticCost = GameConfig.Singleton.InteractionConfig["buildPlantIsland_plasticCost"];
                var fiberCost = GameConfig.Singleton.InteractionConfig["buildPlantIsland_fiberCost"];

                bool hasEnoughPlastic = inventory.GetAmount(ItemTags.plastic) >= plasticCost;
                bool hasEnoughString = inventory.GetAmount(ItemTags.fiber) >= fiberCost;

                bool enoughMat = hasEnoughPlastic && hasEnoughString;
                return enoughMat;
            }
        }
        public bool IsActive => isActive;

        private void StartChatBubble(string[] chat)
        {
            var chatBubble = InterfaceArichives.Archive.InGameUIComponentsManager.ChatBubble;
            var playerPos = InterfaceArichives.Archive.PlayerSystem.OnPlayerPositionChanged;

            chatBubble.StartChat(chat, playerPos);
        }

        public BuildIslandAgent(IObservable<Vector3> OnMouseClicked,IInterfaceArchive archive)
        {
            buildSystem = archive.IBuildSystem;
            onMouseClicked = OnMouseClicked;
            inventory = archive.IInventorySystem;

            OnMouseClicked
                .Skip(1)
                .Where(x => Enough)
                .Where(x => !isActive)
                .Subscribe(x =>
                {
                    var gridPos = buildSystem.newWorldToGridPosition(x);

                    //计算材料消耗
                    var plasticCost_double = GameConfig.Singleton.InteractionConfig["buildPlantIsland_plasticCost"];
                    var plasticCost_int = Convert.ToInt32(plasticCost_double);
                    var fiberCost_double = GameConfig.Singleton.InteractionConfig["buildPlantIsland_fiberCost"];
                    var fiberCost_int = Convert.ToInt32(fiberCost_double);

                    //扣除材料
                    inventory.RemoveItem(ItemTags.plastic, plasticCost_int);
                    inventory.RemoveItem(ItemTags.fiber, fiberCost_int);

                    isActive = true;
                    
                    timer.StartTimeCountdown(5);
                });

            OnMouseClicked
                .Where(x => !Enough)
                .Subscribe(x =>
                {
                    StartChatBubble(new string[] { "我没有足够的材料" });
                });

            timer.OnTimerEnd
                .Subscribe(x =>
                {
                    isActive = false;
                });
        }
    }
}
