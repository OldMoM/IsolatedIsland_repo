using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi 
{
    public class PlayerSystem : MonoBehaviour,IPlayerSystem
    {
        private PlayerMovementPresenter _movement;
        private PlayerBehaviorView _behaivor;
        public PlayerMovementPresenter Movement => _movement;

        Rigidbody rigid;
        public Rigidbody Rigid => rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            _movement = new PlayerMovementPresenter(this);
            _behaivor = gameObject.AddComponent<PlayerBehaviorView>();
        }
    }
}
