using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siwei;
using Peixi;
using Caoye;

public interface IInterfaceArchive
{
    IBuildSystem IBuildSystem { get;}
    IPlayerPropertySystem IPlayerPropertySystem { get; }
    ITimeSystem ITimeSystem { get; }
    IInventorySystem IInventorySystem { get; }
    IArbitorSystem IArbitorSystem { get; }
    IInGameUIComponents InGameUIComponentsManager { get; }
    IPlayerSystem PlayerSystem { get; }
    IAndroidraSystem IAndroidraSystem { get; }
    IBuildSketch IBuildSketch { get; }
    IDialogSystem IDialogSystem { get; }
}
