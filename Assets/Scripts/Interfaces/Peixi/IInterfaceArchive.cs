using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siwei;
using Peixi;

public interface IInterfaceArchive
{
    IBuildSystem IbuildSystem { get; set; }
    IPlayerPropertySystem playerPropertySystem { get; set; }
}
