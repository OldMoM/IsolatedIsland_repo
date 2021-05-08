using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Peixi
{
    public static class Game 
    {
        public static Subject<Unit> onPlayerGetHunger = new Subject<Unit>();
    }
}
