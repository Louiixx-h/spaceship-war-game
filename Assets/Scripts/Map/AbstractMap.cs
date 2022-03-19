using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMap : MonoBehaviourPun
{
    protected IMapImplementation m_MapImplementation;

    public abstract void SetMap(IMapImplementation infoMap);
}
