using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Siwei;
using Peixi;

public interface IInterfaceArchive
{
    IBuildSystem IBuildSystem { get;}
    IPlayerPropertySystem IPlayerPropertySystem { get; }
    ITimeSystem ITimeSystem { get; }
    IInventorySystem IInventorySystem { get; }
    IArbitorSystem IArbitorSystem { get; }
    IInGameUIComponentsInterface InGameUIComponentsManager { get; }
}
