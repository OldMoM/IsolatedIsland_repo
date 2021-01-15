using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi 
{
    public class PlayerSystem : MonoBehaviour,IPlayerSystem
    {
        private PlayerMovementPresenter movement;
        private PlayerBehaviorView behaivor;
        public PlayerMovementPresenter Movement => movement;


        Rigidbody rigid;
        public Rigidbody Rigid => rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            movement = new PlayerMovementPresenter(this);
            behaivor = gameObject.AddComponent<PlayerBehaviorView>();
        }
    }
}
