using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Peixi
{
    public interface IPlayerSystem
    {
        PlayerMovementPresenter Movement { get; }
        Rigidbody Rigid { get; }
        PlayerState PlayerState { get; }
        PlayerStateController StateController { get; }
        IPlayerPropertySystem PlayerPropertySystem { get; }
    }
}
