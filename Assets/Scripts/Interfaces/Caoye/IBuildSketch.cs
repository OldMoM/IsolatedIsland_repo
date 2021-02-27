using System;
using UnityEngine;

namespace Siwei
{
    public interface IBuildSketch
    {
        /// <summary>
        /// 进入建造模式时，激活BuildSketch
        /// </summary>
        IObservable<bool> OnActiveChanged﻿ { get; }
        /// <summary>   
        /// 当鼠标悬停坐标变化时触发此事件，传递当前指 针的所指的世界坐标
        /// </summary>
        IObservable<Vector3> OnMouseHoverPositionChanged﻿ { get; }
        /// <summary>
        ///  当鼠标点击时触发此事件，传递当前指针点击的世界坐标
        /// </summary>
        IObservable<Vector3> OnMouseClicked﻿ { get; }
        /// <summary>
        /// 允许鼠标在指向点处建造Island
        /// </summary>
        bool PermitBuildIsland﻿ { get; set; }
    }
}
