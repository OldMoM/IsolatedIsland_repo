using UniRx;
using System;
using UnityEngine;
using Siwei;
namespace Peixi
{
    public class RestoreIslandProgress
    {
        public IObservable<FacilityType> OnInteractStart => startInteract;
        public IObservable<FacilityType> OnInteractEnd => endInteract;
        public IObservable<float> Progress => timer.OnRatioProcessChanged;

        private Subject<FacilityType> startInteract = new Subject<FacilityType>();
        private Subject<FacilityType> endInteract = new Subject<FacilityType>();
        private ConcurrentTimer timer = new ConcurrentTimer();
        
        private IChatBubble chatBubble=> InterfaceArichives.Archive.InGameUIComponentsManager.ChatBubble;
        private IObservable<Vector3> playerPosition => InterfaceArichives.Archive.PlayerSystem.OnPlayerPositionChanged;
        private int GetMatAmount(string mat)
        {
            var inventory = InterfaceArichives.Archive.IInventorySystem;
            return inventory.GetAmount(mat);
        }

        /// <summary>
        /// 待修复的岛块的Data
        /// </summary>
        /// <param name="islandGridPos"></param>
        public void StartInteract(FacilityData data)
        {
            if (EoughMat)
            {
                startInteract.OnNext(FacilityType.Island);
                timer.OnTimerEnd
                    .First()
                    .Subscribe(x =>
                    {
                        endInteract.OnNext(FacilityType.Island);
                        var island = InterfaceArichives.Archive.IBuildSystem.GetIslandInterface(data.gridPos);
                        island.SetDurabilityTo(100);
                    });

                timer.StartTimeCountdown(4);
            }
            else
            {
                string[] msg = { "没有足够的材料" };
                chatBubble.StartChat(msg, playerPosition);
            }
        }   
        public bool EoughMat
        {
            get
            {
                var plasticAmount = GetMatAmount("Plastic");
                var stringAmount = GetMatAmount("String");
                return false;
            }
        }
    }
}
