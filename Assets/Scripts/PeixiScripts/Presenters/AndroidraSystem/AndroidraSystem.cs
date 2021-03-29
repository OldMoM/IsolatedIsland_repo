using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UniRx;
using System;

namespace Peixi
{
    public class AndroidraSystem : MonoBehaviour,IAndroidraSystem
    {
        private AndroidraStateController state;
        private AndroidraNavPresenter nav;
        private AndroidraControl control;
        private AndroidraBuildAnimationPresenter buildAnim;
        private NavMeshAgent navAgent;
        private NavigateAgent navigateAgent;
        private AndroidraStateControllerAgent controllerAgent;
        private IPlayerSystem playerSystem => InterfaceArichives.Archive.PlayerSystem;

        public AndroidraControl Control
        {
            get
            {
                if (control is null)
                {
                    control = new AndroidraControl();
                }
                return control;
            }
        }
        public AndroidraStateController androidState => state;
        public AndroidraNavPresenter navPresenter => nav;
        public AndroidraBuildAnimationPresenter BuildAnim
        {
            get
            {
                if (buildAnim is null)
                {
                    var navAgent = GetComponent<NavMeshAgent>();
                    Assert.IsNotNull(navAgent);
                    //nav = new AndroidraNavPresenter(navAgent, playerSystem, this,state);
                    buildAnim = new AndroidraBuildAnimationPresenter();
                }
                return buildAnim;
            }
        }
        public AndroidraStateControllerModel createAndroidraStateControllerModel()
        {
            var controllerModel = new AndroidraStateControllerModel();
            controllerModel.OnBuildMsgReceived = control.OnBuildMsgReceived;
            return controllerModel;
        }

        public NavigateAgent CreateNavigateAgent()
        {
            var agent = new NavigateAgent(ref nav, ref state, ref control);
            agent.buildAnimation = this.buildAnim;
            return agent;
        }
        public AndroidraStateControllerAgent CreateAndroidraStateControllerAgent()
        {
            var agnet = new AndroidraStateControllerAgent();
            agnet.controller = androidState;
            agnet.onBuildMsgReceived = control.OnBuildMsgReceived;
            agnet.control = control;
            return agnet;
        }
        // Start is called before the first frame update
        void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            Assert.IsNotNull(navAgent);
            state = new AndroidraStateController(playerSystem, this);

            nav = new AndroidraNavPresenter(navAgent, playerSystem, this, state);
            Assert.IsNotNull(nav, "@ " + transform.name + " nav is null");
            buildAnim = new AndroidraBuildAnimationPresenter();

            //control = new AndroidraControl();

            Control.OnBuildMsgReceived
                .Subscribe(x =>
                {
                    state.SetState(AndroidraState.Building, this.name);
                });

            navigateAgent = CreateNavigateAgent();
            navigateAgent.Init();

            controllerAgent = CreateAndroidraStateControllerAgent();
            controllerAgent.Init();
        }
    }
}
