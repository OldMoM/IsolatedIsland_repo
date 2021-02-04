using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siwei;
using Peixi;

public interface IInterfaceArchive
{
    IBuildSystem IbuildSystem { get; set; }
    IPlayerPropertySystem IplayerPropertySystem { get; set; }
    ITimeSystem ItimeSystem { get; }
    IInventorySystem IinventorySystem { get; }
    IArbitorSystem IarbitorSystem { get; }

    T GetInterface<T>();
}
