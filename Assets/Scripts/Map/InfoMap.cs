using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfoMap", menuName = "Map/InfoMap", order = 1)]
public class InfoMap : ScriptableObject, IMapImplementation
{
    public Sprite m_BackgroundSpace;
    public Sprite BackgroundSpace { 
        get => m_BackgroundSpace;
    }

    public Sprite m_Star;
    public Sprite Star
    {
        get => m_Star;
    }

    public Sprite m_Planet;
    public Sprite Planet
    {
        get => m_Planet;
    }

    public InfoMap GetMap()
    {
        return this;
    }
}
