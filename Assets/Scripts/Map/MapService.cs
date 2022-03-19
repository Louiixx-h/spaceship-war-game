using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapService : AbstractMap
{
    [SerializeField] private SpriteRenderer m_Space;
    [SerializeField] private SpriteRenderer m_Star;
    [SerializeField] private SpriteRenderer m_Planet;
    [SerializeField] private SpriteRenderer m_MaskPlanet;
    
    [Header("STATION")]
    [SerializeField] private bool m_HasStation;
    [SerializeField] private GameObject m_StationBlue;
    [SerializeField] private GameObject m_StationRed;

    [Header("SPAWN STATION")]
    [SerializeField] private Transform m_PositionStationBlue;
    [SerializeField] private Transform m_PositionStationRed;

    [Header("MAP INFO")]
    [SerializeField] private InfoMap m_InfoMap;

    private void Start()
    {
        SetMap(m_InfoMap);
        if (!m_HasStation) return;
        InstantiateStation(m_StationBlue, m_PositionStationBlue);
        InstantiateStation(m_StationRed, m_PositionStationRed);
    }

    public override void SetMap(IMapImplementation infoMap)
    {
        this.m_MapImplementation = infoMap;
        m_Space.sprite = m_MapImplementation.BackgroundSpace;
        m_Planet.sprite = m_MapImplementation.Planet;
        m_Star.sprite = m_MapImplementation.Star;
    }

    void InstantiateStation(GameObject station, Transform transform)
    {
        PhotonNetwork.Instantiate(
            station.name, 
            transform.position, 
            Quaternion.identity, 
            0
        );
    }
}
