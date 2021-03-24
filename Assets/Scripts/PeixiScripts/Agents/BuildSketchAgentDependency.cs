using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class BuildSketchAgentDependency 
    {
        /// <summary>
        /// Androidra建造完成事件
        /// </summary>
        public IObservable<Unit> onAndroidCompleteBuildIsland;
        /// <summary>
        /// string:建造类型
        /// Vector2Int:建造坐标
        /// </summary>
        public Action<string, Vector2Int> AndroidBuildAt;
    }
}
