using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Marker : MonoBehaviour
{


	public enum id
	{
		Zero,
		One,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Eleven,
		Twelve,
		Thirteen,
		Fourteen,
		Fifteen,
		Sixteen,
		Seventeen,
		Eighteen,
		Nineteen,
		Twenty
	}

	public int m_id;
	public id m_buildingId = id.Zero;
	public MapArea m_mapArea = MapArea.Sapphire;
	public Vector3 m_normalizedPosition;
	public GameObject m_telePortTo;
	public bool m_isPortal = false;

	public enum MapArea
	{
		Sapphire,
		Emerald,
		Ruby,
		Amber
	}

	[HideInInspector]
	public BuildingInfo m_info;

	void Start ()
	{
		//If we are not a building, don't get any building data...
		if (m_buildingId == id.Zero)
			return;

		DataReaderMap dataObj = FindObjectOfType<DataReaderMap> ();

		if (dataObj == null) {
			Debug.LogError ("No database found, killing myself now...");
			Destroy (this);
		}
		m_info = dataObj.m_data [(int)m_buildingId];
	}

	public Marker GetMarker ()
	{
		return this;
	}


}
