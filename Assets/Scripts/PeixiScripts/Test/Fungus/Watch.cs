using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Fungus
{
    public class Watch : Command
    {
        public Subject<Unit> subject;
    }
}
