using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapImplementation
{
    public Sprite BackgroundSpace
    {
        get;
    }
    public Sprite Star
    {
        get;
    }
    public Sprite Planet
    {
        get;
    }
    public InfoMap GetMap();
}
