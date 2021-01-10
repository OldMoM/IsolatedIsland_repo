using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    /// 处理Island的数据逻辑
    /// </summary>
    public class IslandPresenter : MonoBehaviour,IIsland
    {
        [SerializeField]
        IntReactiveProperty durability_current = new IntReactiveProperty(100);
        public int Durability_current
        {
            get => durability_current.Value;
        }
        public void SetDurabilityTo(int targetValue)
        {
            var _current = durability_current.Value;
            _current = Mathf.Clamp(targetValue, -1, durability_max + 1);
            print(_current);
            durability_current.Value = _current;
        }
        public IObservable<int> OnDurabilityChanged => durability_current;

        private int durability_max = 100;
        private Vector2Int m_gridPos;
        private bool isActive = false;

        System.IDisposable endTimer;
        // Start is called before the first frame update
        private void OnEnable()
        {
            endTimer = Observable.Interval(System.TimeSpan.FromSeconds(1))
               .TakeWhile(x => durability_current.Value > 0)
               .Subscribe(x =>
               {
                   durability_current.Value--;

               }).AddTo(this);

            durability_current
                .Where(z => z == 0)
                .Where(y => isActive)
                .Subscribe(x =>
                {
                    endTimer.Dispose();
                    isActive = false;
                    IBuildSystem ibuildSystem = FindObjectOfType<BuildSystem>();
                    Assert.IsNotNull(ibuildSystem);
                    ibuildSystem.RemoveIslandAt(m_gridPos);
                    Destroy(gameObject);
                }).AddTo(this);
        }
        public void Active(Vector2Int gridPos,int durability_max = 100)
        {
            durability_current.Value = durability_max;
            m_gridPos = gridPos;
            isActive = true;
        }
    }
    public interface IIsland
    {
        /// <summary>
        /// 岛块当前的耐久度
        /// </summary>
        int Durability_current { get; }
        /// <summary>
        /// 岛块耐久度改变时，激活此事件
        /// </summary>
        IObservable<int> OnDurabilityChanged { get; }
        /// <summary>
        /// 岛块创建时默认为关闭状态，调用此方法激活
        /// </summary>
        /// <param name="gridPos"> Island的网格坐标</param>
        /// <param name="durability_max"> Island的初始耐久度</param>
        void Active(Vector2Int gridPos, int durability_max = 100);
        /// <summary>
        /// 设置岛块的耐久度至目标值
        /// </summary>
        /// <param name="targetValue">耐久度目标值</param>
        void SetDurabilityTo(int targetValue);
    }
}
