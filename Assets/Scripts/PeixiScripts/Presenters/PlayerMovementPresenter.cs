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

                    velocity.Value = RotateVelocityBy(_velocity, -60);
                });
        }
        /// <summary>
        /// 旋转XOZ平面的移动速度
        /// </summary>
        /// <param name="velocity">原始输入数据</param>
        /// <param name="degree">顺时针旋转为负</param>
        /// <returns></returns>
        private Vector3 RotateVelocityBy(Vector3 velocity, float degree)
        {
            var _velocity = velocity;
            var x_rotated = _velocity.x * Mathf.Cos(degree * Mathf.Rad2Deg) - _velocity.z * Mathf.Sin(degree * Mathf.Rad2Deg);
            x_rotated = -x_rotated;
            var y = m_playerSystem.Rigid.velocity.y;
            var z_rotated = _velocity.x * Mathf.Sin(degree * Mathf.Rad2Deg) + _velocity.z * Mathf.Cos(degree * Mathf.Rad2Deg);
            z_rotated = -z_rotated;
            var velocity_rotated = new Vector3(x_rotated, y, z_rotated);
            return velocity_rotated;
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
