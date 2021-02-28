using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    /// <summary>
    ///  小机器人上下浮动为正弦运动
    /// </summary>
    public class AndroidFloatingEffect :MonoBehaviour
    {
        [Range(0,0.5f)]
        public float amplitude;
        public float frequency;
        public float time;
        public IDisposable EnableAndroidModelFloating(Transform androidraModel)
        {
            var positionStart_y = androidraModel.position.y;
            IDisposable floatingProcess = 
            Observable.EveryLateUpdate()
                .Subscribe(x =>
                {
                    var position_y = amplitude * Mathf.Sin(time * 2 * Mathf.PI * frequency) + positionStart_y;
                    var position = androidraModel.position;
                    position.y = position_y;
                    androidraModel.position = position;
                    time += Time.deltaTime;
                });
            
            return floatingProcess;
        }

        private void Start()
        {
            EnableAndroidModelFloating(transform);
        }
    }
}