using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {

   // public MarkerClass m_markerInfo;
    public int m_id;
    public GameObject m_telePortTo;
    public enum MapArea
    {
        Town,
        Country
    }
    public MapArea m_mapArea = MapArea.Country;
    public Vector2 m_normalizedPosition;

}
