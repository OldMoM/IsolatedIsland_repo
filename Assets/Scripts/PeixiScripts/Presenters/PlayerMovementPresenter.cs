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
        private Vector3 faceDir;
        public PlayerMovementPresenter(IPlayerSystem playerSystem)
        {
            m_playerSystem = playerSystem;
            Init();
        }

        PlayerMovementModel movementModel = new PlayerMovementModel();
        PlayerStateModel stateModel = new PlayerStateModel();

        IObservable<PlayerState> OnPlayerStateChanged => m_playerSystem.StateController.onStateChanged;
        PlayerState PlayerState => m_playerSystem.StateController.playerState.Value;
        public Vector3 FaceDirection => faceDir;
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
        private void Init()
        {
            React(OnPlayerInput)
                .React(OnInteractStart);
        }
        PlayerMovementPresenter React(Action action)
        {
            action();
            return this;
        }
        void OnPlayerInput()
        {
            Observable.EveryFixedUpdate()
              .Where(x => PlayerState != PlayerState.InteractState)
              .Subscribe(x =>
              {
                  Vector3 _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                  _direction = -_direction.normalized;
                  _direction = RotateVelocityBy(_direction, -60);

                  var _velocity = _direction * moveSpeed;
                  //velocity.Value = RotateVelocityBy(_velocity, -60);
                  velocity.Value = _velocity;
                  ComputeFaceDir(_direction);
              });
        }
        void OnInteractStart()
        {
            OnPlayerStateChanged
               .Where(x => x == PlayerState.InteractState)
               .Subscribe(x =>
               {
                   velocity.Value = Vector3.zero;
               });
        }
        /// <summary>
        /// ISO坐标朝向
        /// </summary>
        /// <param name="moveDir"></param>
        void ComputeFaceDir(Vector3 moveDir)
        {
            if (moveDir == Vector3.zero)
            {
                return;
            }

            faceDir = moveDir;

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
    }
}
