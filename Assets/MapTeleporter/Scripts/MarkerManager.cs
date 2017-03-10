using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{

	public GameObject m_player;
	public Renderer m_worldArea;
	public Renderer m_mapArea;
	public GameObject m_mapMarkerPrefab;
	public Vector3 m_offsetBy = Vector3.zero;

	public Dictionary<int, Marker> m_worldMarkers;
	public Animator[] m_mapMarkers;

	private bool m_isReady = false;
	private int _currentNumber = 0;
	private int _lastNumber = 0;
	private float m_threshold = .1f;

	void Start ()
	{
		GetMarkersAndNormalizedPositions ();
		MakeAndPlaceMapMarkers ();
	}

	void Update ()
	{

		if (!m_isReady)
			return;

		float f = Input.GetAxisRaw ("Mouse ScrollWheel");
		if (f > 0) {
			_currentNumber++;
			if (_currentNumber >= m_mapMarkers.Length)
				_currentNumber = m_mapMarkers.Length - 1;
		} else if (f < 0) {
			_currentNumber--;
			if (_currentNumber < 0) {
				_currentNumber = 0;
			}
		}

		if (_currentNumber != _lastNumber) {
			m_mapMarkers [_currentNumber].SetTrigger ("Select");
			//Debug.Log ("selected " + m_mapMarkers [_currentNumber].name + ", " + _currentNumber.ToString () + ",last is " + _lastNumber.ToString ());
			m_mapMarkers [_lastNumber].SetTrigger ("Deselect");
			_lastNumber = _currentNumber;
		} 

		if (Input.GetButtonDown ("Fire1")) {

			SteamVR_Fade.View (Color.black, 0);
			SteamVR_Fade.View (Color.clear, 1);
			m_player.transform.position = m_worldMarkers [_currentNumber].m_telePortTo.transform.position;
		}

			
	}

	void GetMarkersAndNormalizedPositions ()
	{
		Marker[] temp = GetComponentsInChildren<Marker> ();
		m_worldMarkers = new Dictionary<int, Marker> ();

		for (int i = 0; i < temp.Length; i++) {
			Marker m = temp [i];
			m.m_id = i;
			m.m_telePortTo = m.gameObject;
			m.transform.name = "TelePorter_" + i.ToString ();

			//Get the coordinates as a percent of the world size plane (normalize) This may require that the world map be at position 0,0,0 for it to work right
			float xPos = Mathf.InverseLerp (0, m_worldArea.bounds.size.x, m.transform.position.x + m_worldArea.bounds.extents.x);
			float yPos = Mathf.InverseLerp (0, m_worldArea.bounds.size.z, m.transform.position.z + m_worldArea.bounds.extents.z);
			float zPos = Mathf.Abs (xPos - .5f) * 2f;

			m.m_normalizedPosition = new Vector3 (xPos, yPos, zPos);
			m_worldMarkers.Add (i, m);

		}
	}

	void MakeAndPlaceMapMarkers ()
	{
		m_mapMarkers = new Animator[m_worldMarkers.Count];

		//make a parent for the map in case the map is scaled non-uniformly
		GameObject g = new GameObject ("Map and Markers");
		g.transform.position = m_mapArea.transform.position;
		g.transform.rotation = m_mapArea.transform.rotation;
		m_mapArea.gameObject.transform.SetParent (g.transform);

		GameObject q = new GameObject ("Markers");
		q.transform.SetParent (g.transform);
		q.transform.localPosition = Vector3.zero;
		q.transform.localEulerAngles = Vector3.zero;

		for (int i = 0; i < m_worldMarkers.Count; i++) {

			float xPos = Mathf.Lerp (-m_mapArea.bounds.extents.x, m_mapArea.bounds.extents.x, m_worldMarkers [i].m_normalizedPosition.x);
			float yPos = Mathf.Lerp (-m_mapArea.bounds.extents.y, m_mapArea.bounds.extents.y, m_worldMarkers [i].m_normalizedPosition.y);
			float zPos = Mathf.Lerp (-m_mapArea.bounds.extents.z, m_mapArea.bounds.extents.z, m_worldMarkers [i].m_normalizedPosition.z);

			GameObject go = Instantiate (m_mapMarkerPrefab, q.transform);
			go.transform.localPosition = new Vector3 (xPos, yPos, zPos) + m_offsetBy;

			m_mapMarkers [i] = go.GetComponentInChildren<Animator> () as Animator;
		}

		m_isReady = true;
	}
}

