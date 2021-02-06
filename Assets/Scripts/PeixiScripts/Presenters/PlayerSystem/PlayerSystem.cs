using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi 
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        private PlayerMovementPresenter _movement;
        private PlayerBehaviorView _behaivor;
        private PlayerStateController stateController = new PlayerStateController();
        private PlayerState playerState;
        private PlayerPropertySystem property = new PlayerPropertySystem();

        public PlayerMovementPresenter Movement => _movement;

        Rigidbody rigid;
        public Rigidbody Rigid => rigid;
        public PlayerState PlayerState => playerState;

        public PlayerStateController StateController => stateController;

        public IPlayerPropertySystem PlayerPropertySystem => property;
   

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            _movement = new PlayerMovementPresenter(this);
            _behaivor = gameObject.AddComponent<PlayerBehaviorView>();

            stateController.Init();
        }

    }
}
