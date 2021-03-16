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
                bool hasEnoughPlastic = inventory.GetAmount("Plastic") >= 15;
                bool hasEnoughString = inventory.GetAmount("String") >= 8;

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
                    Debug.Log(x);
                    var gridPos = buildSystem.newWorldToGridPosition(x);
                    //消耗材料
                    inventory.RemoveItem("Plastic", 15);
                    inventory.RemoveItem("String", 8);

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
