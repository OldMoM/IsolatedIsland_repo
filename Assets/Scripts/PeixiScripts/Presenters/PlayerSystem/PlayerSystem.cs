using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi 
{
    public class PlayerSystem : MonoBehaviour,IPlayerSystem
    {
        private PlayerMovementPresenter movement;
        public PlayerMovementPresenter Movement => movement;

        void Awake()
        {
            movement = new PlayerMovementPresenter(this);
            gameObject.AddComponent<PlayerBehaviorView>();
        }
    }
}
