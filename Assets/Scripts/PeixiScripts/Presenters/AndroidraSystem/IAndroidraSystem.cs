using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Peixi 
{
    public interface IAndroidraSystem 
    {
        AndroidraControl Control { get; }
        AndroidraStateController androidState { get; }
        AndroidraBuildAnimationPresenter BuildAnim { get; }
    }
}

