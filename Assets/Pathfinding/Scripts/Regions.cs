using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Region
{
	public string name;
	public LayerMask regionMask;
	public float movementCost;
	public Color gizmosColor;
}

[CreateAssetMenu()]
public class Regions : ScriptableObject {
	public List<Region> regions;
}
