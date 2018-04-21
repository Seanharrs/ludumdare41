using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerTest : MonoBehaviour {

	public Map maps;

	public Transform startTransform;
	public Transform endTransform;

	List<Node> path = new List<Node>();

	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			path.Clear ();
			path = PathFinding.DoPathFind_AStar (
				maps.GetNodeFromPosition ((startTransform.position)),
				maps.GetNodeFromPosition ((endTransform.position))
				, maps.Nodes
			);
		}
	}

	void OnDrawGizmos ()
	{
		if (maps.Nodes != null) {
			for (int y = 0; y < maps.Height; y++) {
				for (int x = 0; x < maps.Width; x++) {
					if (path != null) {
						Vector3 worldPos = maps.mapTransform.position - Vector3.right * maps.gridSize * (maps.Width / 2 - x - 0.5f) - Vector3.up * maps.gridSize * (maps.Height / 2 - y - 0.5f);
						if (path.Contains (maps.Nodes [x, y])) {
							Gizmos.color = Color.black;
							Gizmos.DrawCube (worldPos, Vector3.one * maps.gridSize * 0.8f);
							continue;
						}
					}
				}
			}
		}
	}
}
