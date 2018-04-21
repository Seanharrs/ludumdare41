using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPathGenerator : MonoBehaviour {

	public float criticalDstToSeekNextTarget;
	public bool isCyclic;

	public Path path { get; protected set;}

	public List<Transform> points;

	void Awake () {
		if (points != null) {

			List<Vector2> wayPoints = new List<Vector2> ();
			for (int i = 0; i < points.Count; i++) {
				wayPoints.Add ((Vector2)points [i].position);
			}

			path = new Path (wayPoints.ToArray (), criticalDstToSeekNextTarget, isCyclic);
		}
	}
	
	void OnDrawGizmos () {
		if (points != null) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere (points [0].position, 0.7f);
			for (int i = 0; i < points.Count - 1; i++) {
				Gizmos.color = Color.red;
				Gizmos.DrawLine (points [i].position, points [i + 1].position);
				Gizmos.color = Color.black;
				Gizmos.DrawSphere (points [i].position, 0.7f);
			}
			Gizmos.DrawSphere (points [points.Count - 1].position, 0.7f);
		}
	}
}
