using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using Siwei;

namespace Peixi 
{
    //整合和玩家功能模块
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        #region//内部模块和组件
        private PlayerMovementPresenter _movement;
        private PlayerBehaviorView _behaivor;
        private PlayerStateController stateController = new PlayerStateController();
        private PlayerPropertySystem property = new PlayerPropertySystem();
        private PlayerAnimationView animationView;
        private Rigidbody rigid;
        private PlayerSystemAgent systemAgents;
        private Vector3ReactiveProperty onPositionChanged = new Vector3ReactiveProperty();

        private HealthAgent healthAgent;
        private HungerAgent hungerAgent;
        private ThirstAgent thirstAgent;
        private PleasureAgent pleasureAgent;
        #endregion

        #region//接口实现
        public PlayerMovementPresenter Movement => _movement;
        public Rigidbody Rigid => rigid;
        public PlayerStateController StateController => stateController;
        public IPlayerPropertySystem PlayerPropertySystem => property;
        public IObservable<Vector3> OnPlayerPositionChanged => onPositionChanged;
   
        #endregion

        #region//内部模块私有初始化方法
        private PlayerAnimationView createAnimationView()
        {
            var animator = transform.Find("Character_Player").GetComponent<Animator>();
            Assert.IsNotNull(animator);
            return new PlayerAnimationView(animator, 
                                           stateController,
                                           _movement.OnFaceDirectionScreenChanged);
        }

        private Rigidbody createRigidbody()
        {
            var rigid = GetComponent<Rigidbody>();
            Assert.IsNotNull(rigid);
            return rigid;
        }

        private PlayerMovementPresenter createPlayerMovementPresenter()
        {
            return new PlayerMovementPresenter(this);
        }

        private PlayerBehaviorView createPlayerBehaviorView()
        {
            var behavior = gameObject.AddComponent<PlayerBehaviorView>();
            return behavior;
        }

        private PlayerStateController createPlayerStateController(PlayerMovementPresenter movement)
        {
            stateController = new PlayerStateController();
            stateController.Init(movement);
            return stateController;
        }

        private AgentDependency CreateDependency()
        {
            AgentDependency dependency = new AgentDependency();

            dependency.speed = _movement.moveSpeed;
            dependency.playerPropertySystem = property;
            dependency.isDay = new BoolReactiveProperty(false);
            dependency.onRainDay = new Subject<Unit>();
            return dependency;
        }
        #endregion

        void Awake()
        {
            rigid           = createRigidbody();
            _movement       = createPlayerMovementPresenter();
            _behaivor       = createPlayerBehaviorView();
            stateController = createPlayerStateController(_movement);
            animationView   = createAnimationView();
            systemAgents = new PlayerSystemAgent(this);
        }
        void Start()
        {
            Observable.EveryLateUpdate()
                .Subscribe(x =>
                {
                    onPositionChanged.Value = transform.position;
                });

            healthAgent = new HealthAgent(CreateDependency());
            hungerAgent = new HungerAgent(CreateDependency());
            thirstAgent = new ThirstAgent(CreateDependency());
            pleasureAgent = new PleasureAgent(CreateDependency());

        }
    }
}
