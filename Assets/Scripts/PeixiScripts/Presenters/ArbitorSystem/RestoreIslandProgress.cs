using UniRx;
using System;
using UnityEngine;
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

        /// <summary>
        /// 待修复的岛块的Data
        /// </summary>
        /// <param name="islandGridPos"></param>
        public void StartInteract(FacilityData data)
        {
            if (EoughMat)
            {
                Debug.Log("开始修复岛块计时");
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

            }
        }   
        public bool EoughMat
        {
            get
            {
                return true;
            }
        }
    }
}
