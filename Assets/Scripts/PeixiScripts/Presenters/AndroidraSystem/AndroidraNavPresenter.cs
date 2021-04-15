using UnityEngine.AI;
using UniRx;
using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    public class AndroidraNavPresenter
    {
        private NavMeshAgent _agent;
        private IAndroidraSystem _system;

        private BoolReactiveProperty isMoving = new BoolReactiveProperty();

        private Subject<Unit> onAndroidraStartMove = new Subject<Unit>();
        private Subject<Unit> onAndroidraEndMove = new Subject<Unit>();
        private Subject<ValueTuple<string, Vector3>> onAndroidraReachBuildTarget = new Subject<(string, Vector3)>();

        private Subject<Vector3> onAndroidReachTarget = new Subject<Vector3>();


        private string buildFacilityType;

        private Vector3 target;
        private IPlayerSystem _playerSystem;
        private Func<Vector2Int, Vector3> _gridPosToWorld;
        private AndroidraStateController _state => _system.androidState;
        private AndroidraControl _control => _system.Control;

        private float distanceToTarget;
        private BoolReactiveProperty hasReachedTarget = new BoolReactiveProperty();

        private AndroidraStateController state;

       
        Func<Vector2Int, Vector3> gridPosToWorld
        {
            get
            {
                if (_gridPosToWorld is null)
                {
                    _gridPosToWorld = InterfaceArichives.Archive.IBuildSystem.newGridToWorldPosition;
                }
                return _gridPosToWorld;
            }
        }
        public Vector3 Target 
        {
            get => target;
            set
            {
                target = value;
            }
        }
        public IObservable<Unit> OnAndroidraStartMove => onAndroidraStartMove;
        public IObservable<Unit> OnAndroidraEndMove => onAndroidraEndMove;
        public IObservable<Vector3> OnAndroidraReachTarget => onAndroidReachTarget;
        /// <summary>
        /// The first param is the facility type.\n The second param is the build position.
        /// </summary>
        public IObservable<ValueTuple<string, Vector3>> OnAndroidraReachBuildTarget => onAndroidraReachBuildTarget;
        public IObservable<bool> OnReachedTarget => hasReachedTarget;

        IDisposable disposable;
        public AndroidraNavPresenter(NavMeshAgent agent,IPlayerSystem playerSystem, IAndroidraSystem system,AndroidraStateController stateController)
        {
            _agent = agent;
            _playerSystem = playerSystem;
            _system = system;

            state = stateController;

            Observable.EveryUpdate()
                .Where(x => _state.State == AndroidraState.Idle || _state.State == AndroidraState.Follow)
                .Subscribe(x =>
                {
                    var playerPos = _playerSystem.Rigid.position;
                    var playerFaceDir = _playerSystem.Movement.FaceDirection;
                    target = playerPos + playerFaceDir * -0.5f + Vector3.up * 2;
                });

            Observable.EveryUpdate()
                .Where(x=> _state.State != AndroidraState.Sleep)
                .Subscribe(x =>
                {
                    _agent.SetDestination(target);
                    hasReachedTarget.Value = _agent.remainingDistance <= 0.5f;
                });

            Observable.EveryLateUpdate()
                .Subscribe(x =>
                {
                    hasReachedTarget.Value = distanceToTarget <= 0.1f;
                });

            isMoving
                .Skip(1)
                .Subscribe(x =>
                {
                    if (x)
                    {
                        onAndroidraStartMove.OnNext(Unit.Default);
                    }
                    else
                    {
                        onAndroidraEndMove.OnNext(Unit.Default);
                    }
                });
        }

        //void OnBuildMsgReceived()
        //{
        //    _control.OnBuildMsgReceived
        //        .Subscribe(x =>
        //        {
        //            //record msg data
        //            var target_world = gridPosToWorld(x.Item2);
        //            buildFacilityType = x.Item1;

        //            //delay 5 frames to invoke
        //            Observable.Timer(TimeSpan.FromTicks(5))
        //            .First()
        //            .Subscribe(y =>
        //            {
        //                target = target_world;
        //            });
        //        });
        //}
        //void OnAndroidReachBuildTarget()
        //{
        //    hasReachedTarget
        //        .Where(x => !x)
        //        .Where(x => _system.androidState.State == AndroidraState.Building)
        //        .Subscribe(x =>
        //        {
        //            Debug.Log("Androidra has reached the building target and start to build facility");
        //            onAndroidraReachBuildTarget.OnNext(new ValueTuple<string, Vector3>(buildFacilityType,target));
        //        });
        //}
    }
}
