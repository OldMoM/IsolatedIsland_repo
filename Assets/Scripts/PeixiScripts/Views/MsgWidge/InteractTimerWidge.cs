using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Peixi
{
    public class InteractTimerWidge : MonoBehaviour
    {
        Image timeCircleImange;
        GameObject timeCircleGameObject;
        FacilityInteractionAgent agent;

        public bool isCounting;
        IDisposable countProcess;
        // Start is called before the first frame update
        void Start()
        {
            var timeCircleImange_trans = transform.Find("timerCircle");
            timeCircleImange = timeCircleImange_trans.GetComponent<Image>();
            timeCircleGameObject = timeCircleImange_trans.gameObject;

            agent = InterfaceArichives.Archive.IArbitorSystem.facilityInteractAgent;
            OnFoodPlantInteractionStart();
        }

        void OnFoodPlantInteractionStart()
        {
            agent.FoodPlantInteract.OnInteractStart
                .Subscribe(y =>
                {
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
    }
}
