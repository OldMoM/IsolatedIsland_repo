using UniRx;
using System;

namespace Peixi
{
    public class TentInteractionProgress 
    {
        public void StartInteract()
        {
            var timerSystem = InterfaceArichives.Archive.ITimeSystem;
            timerSystem.StartNight();
        }
    }
}
