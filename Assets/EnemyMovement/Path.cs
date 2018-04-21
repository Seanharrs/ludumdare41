using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

	public Line[] lines;

	public Path  (Vector2[] wayPoints,float distanceToPerpendicularLineFromEndpoint,bool isCyclic = false) {
		if (!isCyclic) {
			lines = new Line[wayPoints.Length - 1];
			for (int i = 0; i < lines.Length; i++) {
				lines [i] = new Line (wayPoints [i], wayPoints [i + 1], distanceToPerpendicularLineFromEndpoint);
			}
		} else {
			lines = new Line[wayPoints.Length ];

			for (int i = 0; i < lines.Length; i++) {
				lines [i] = new Line (wayPoints [i], wayPoints [(i + 1)%wayPoints.Length], distanceToPerpendicularLineFromEndpoint);
			}
		}
	}



	public void DrawGizmos () {
		if (lines != null) {
			for (int i = 0; i < lines.Length; i++) {
				Gizmos.color = Color.white;
				Gizmos.DrawSphere (lines [i].starPoint, 0.1f);
				Gizmos.DrawSphere (lines [i].endPoint, 0.1f);

				Gizmos.color = Color.red;
				Gizmos.DrawLine (lines [i].starPoint, lines [i].endPoint);
				Gizmos.color = Color.white;
				lines [i].DrawGizmos ();
			}
		}
	}

}
