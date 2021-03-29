using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Peixi
{
    /// <summary>进行互动时，计时UI</summary>
    public class InteractTimerWidge : MonoBehaviour
    {
        Image timeCircleImange;
        GameObject timeCircleGameObject;
        FacilityInteractionAgent agent;
        ConcreteDistillerInteractAgent distillerAgent;
        RestoreIslandProgress restoreIslandProgress;

        public bool isCounting;
        IDisposable countProcess;
        private Vector3 targetPosition => InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent.targetData.position;

        private void OnInteractStart(string interactType)
        {
            isCounting = true;
            timeCircleGameObject.SetActive(true);
            FollowTarget();
        }
        private void OnInteractCompleted()
        {
            isCounting = false;
            timeCircleGameObject.SetActive(false);
        }
        private void OnInteractTicking(float progress)
        {
            timeCircleImange.fillAmount = progress;
        }

        private void RestoreIslandAgent()
        {
            agent.RestoreIslandProgress
                 .OnInteractStart
                 .Subscribe(x =>
                 {
                     OnInteractStart("RestoreIsland");
                 });

            agent.RestoreIslandProgress
                 .Progress
                 .Where(x => isCounting)
                 .Subscribe(OnInteractTicking);

            agent.RestoreIslandProgress
                .OnInteractEnd
                .Subscribe(x =>
                {
                    OnInteractCompleted();
                });
        }

        // Start is called before the first frame update
        void Start()
        {
            var timeCircleImange_trans = transform.Find("timerCircle");
            timeCircleImange = timeCircleImange_trans.GetComponent<Image>();
            timeCircleGameObject = timeCircleImange_trans.gameObject;

            agent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;

            OnFoodPlantInteractionStart();

            agent.DistillerAgent
                .OnRatioTimeChanged
                .Where(x=>isCounting)
                .Subscribe(x =>
                {
                    timeCircleImange.fillAmount = x;
                });

            agent.DistillerAgent
                .OnInteractStart
                .Subscribe(x =>
                {
                    isCounting = true;
                    timeCircleGameObject.SetActive(true);
                    FollowTarget();
                });

            agent.DistillerAgent
                .OnInteractEnd
                .Subscribe(x =>
                {
                    isCounting = false;
                    timeCircleGameObject.SetActive(false);
                });

            RestoreIslandAgent();
        }
        void OnFoodPlantInteractionStart()
        {
            agent.FoodPlantInteract.OnInteractStart
                .Subscribe(y =>
                {
                    FollowTarget();
                    Counting(3);
                }); 
        }
        void Counting(int lastTime)
        {
            isCounting = true;
            float time = 0;
            timeCircleGameObject.SetActive(true);
            countProcess = Observable.EveryLateUpdate()
                .Where(x => isCounting)
                .Subscribe(x =>
                {
                    time += Time.deltaTime;
                    var fillValue = time / lastTime;
                    timeCircleImange.fillAmount = fillValue;
                    if (time >= lastTime)
                    {
                        isCounting = false;
                        timeCircleGameObject.SetActive(false);
                        agent.FoodPlantInteract.EndInteract();
                        countProcess.Dispose();
                    }
                });
        }
        void FollowTarget()
        {
            var screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
            var timeCirclePosition = screenPosition + new Vector3(0, 200, 0);
            timeCircleImange.transform.position = timeCirclePosition;
        }
    }
}
