using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    [Serializable]
    public class PlayerMovementPresenter 
    {
        private IPlayerSystem m_playerSystem;
        [SerializeField]
        private Vector3ReactiveProperty velocity = new Vector3ReactiveProperty();
        public PlayerMovementPresenter(IPlayerSystem playerSystem)
        {
            m_playerSystem = playerSystem;
            Active();
        }

        PlayerMovementModel movementModel = new PlayerMovementModel();
        PlayerStateModel stateModel = new PlayerStateModel();

        public float moveSpeed = 5;
        [Obsolete]
        public ReactiveProperty<Vector3> ObserleteVelocity
        {
            get => movementModel.velocity;
        }
        /// <summary>
        /// 当玩家速度改变时触发此事件
        /// </summary>
        public IObservable<Vector3> OnVelocityChanged => velocity;
        /// <summary>
        /// 玩家当前的速度
        /// </summary>
        public Vector3 Velocity => velocity.Value;
        private void Active()
        {
            Observable.EveryFixedUpdate()
                .Subscribe(x =>
                {
                    Vector3 _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    _direction = -_direction.normalized; ;

                    var _velocity = _direction * moveSpeed;
                    _velocity.y = m_playerSystem.Rigid.velocity.y;

                    velocity.Value = _velocity;
                });
        }

        private void Start()
        {
            stateModel.playerState
                .Where(x => stateModel.playerState.Value == PlayerState.MotionState)
                .Subscribe(x =>
                {

                });

            

            #region//InteractState
            stateModel.playerState
            .Where(x => stateModel.playerState.Value == PlayerState.InteractState)
            .Subscribe(x =>
            {
                movementModel.velocity.Value = Vector3.zero;
            });
            #endregion
        }
    }
}
