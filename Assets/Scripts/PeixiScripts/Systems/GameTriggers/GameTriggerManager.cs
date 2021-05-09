using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi {
    public class GameTriggerManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var onHungerChanged = InterfaceArichives.Archive.IPlayerPropertySystem.OnSatietyChanged;
            GameFailedTrigger.OnGameFailedTrigged(onHungerChanged);
        }
    }
}
