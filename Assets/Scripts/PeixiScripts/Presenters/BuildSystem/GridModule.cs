using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

namespace Peixi
{
    /// <summary>
    /// 一个字典数据容器，提供基础的读写和事件
    /// </summary>
    /// <typeparam name="T1">Key类型</typeparam>
    /// <typeparam name="T2">Value类型</typeparam>
    public class GridModule<T1, T2>
    {
        protected GridModuleModel<T1, T2> model = new GridModuleModel<T1, T2>();
        public bool CheckHasTheKey(T1 key)
        {
            return model.gridData.ContainsKey(key);
        }
        public T2 RemoveData(T1 key, string exMsg = "未能在GridData中找到对应值，确保该Key存在")
        {
            var hasKey = CheckHasTheKey(key);
            if (!hasKey)
            {
                throw new System.Exception(exMsg);
            }
            var tempData = model.gridData[key];
            model.gridData.Remove(key);
            return tempData;
        }
        public void AddData(T1 key, T2 value, string exMsg = "该Key值已经存在于GridData中")
        {
            var hasKey = CheckHasTheKey(key);
            if (hasKey)
            {
                throw new System.Exception(key + "处已经添加了数据，无法在重复添加");
            }
            model.gridData.Add(key, value);
        }
        public IObservable<DictionaryAddEvent<T1, T2>> OnDataAdded => model.gridData.ObserveAdd();
        public IObservable<DictionaryRemoveEvent<T1,T2>> OnDataRemoved => model.gridData.ObserveRemove();
    }
    public class GridModuleModel<T1, T2>
    {
        public ReactiveDictionary<T1, T2> gridData = new ReactiveDictionary<T1, T2>();
    }
}
