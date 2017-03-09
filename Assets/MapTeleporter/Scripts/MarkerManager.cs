using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour {

    public Renderer m_worldArea;
    public Renderer m_mapArea;
    public GameObject m_mapMarkerPrefab;

    public Dictionary<int, Marker> m_worldMarkers;
    public Marker[] m_mapMarkers;


    void Start()
    {
        Marker[] temp = GetComponentsInChildren<Marker>();
 //       Debug.Log("Renderer is " + m_worldArea.bounds.size);

        m_worldMarkers = new Dictionary<int, Marker>();

        for(int i = 0; i < temp.Length; i++ )
        {
            Marker m = temp[i];
            m.m_id = i;
            m.m_telePortTo = m.gameObject;
            m.transform.name = "TelePorter_" + i.ToString();

            //Get the coordinates as a percent of the world size plane (normalize) This may require that the world map be at position 0,0,0 for it to work right
            float xPos = Mathf.InverseLerp(0, m_worldArea.bounds.size.x,m.transform.position.x + m_worldArea.bounds.extents.x);
            float yPos = Mathf.InverseLerp(0, m_worldArea.bounds.size.z, m.transform.position.z + m_worldArea.bounds.extents.z);

            m.m_normalizedPosition = new Vector2(xPos, yPos);
            m_worldMarkers.Add(i, m);

        }

        MakeAndPlaceMapMarkers();
    }

    void MakeAndPlaceMapMarkers()
    {
        m_mapMarkers = new Marker[m_worldMarkers.Count];

        //make a parent for the map in case the map is scaled non-uniformly
        GameObject g = new GameObject();
        g.transform.position = m_mapArea.transform.position;
        g.transform.rotation = m_mapArea.transform.rotation;
        m_mapArea.gameObject.transform.SetParent(g.transform);

        for (int i = 0; i < m_worldMarkers.Count; i++) {

            float xPos = Mathf.Lerp(-m_mapArea.bounds.extents.x, m_mapArea.bounds.extents.x, m_worldMarkers[i].m_normalizedPosition.x);
            float yPos = Mathf.Lerp(-m_mapArea.bounds.extents.y, m_mapArea.bounds.extents.y, m_worldMarkers[i].m_normalizedPosition.y);
            GameObject go = Instantiate(m_mapMarkerPrefab,g.transform);
            go.transform.localPosition = new Vector3(xPos, yPos, 0);
            m_mapMarkers[i] = go.GetComponent<Marker>();

        }
    }
}

