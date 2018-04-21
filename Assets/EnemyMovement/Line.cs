using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line  {

	const float verticalLineGradient = 1e5f;

	public Vector2 starPoint;
	public Vector2 endPoint;

	public float gradientPerpendicularToLine;
	public float gradientLine;

	Vector2 pointOnLine_1;
	Vector2 pointOnLine_2;
	Vector2 previousPerpendiculatPOintOnLine;

	public Line (Vector2 startPoint,Vector2 endPoint,float distanceToPerpendicularLineFromEndpoint) {
		this.starPoint = startPoint;
		this.endPoint = endPoint;

		float dx = endPoint.x - startPoint.x;
		float dy = endPoint.y - startPoint.y;

		if (dx == 0) {
			gradientLine = verticalLineGradient;
		} else {
			gradientLine = dy / dx;
		}

		if (gradientLine == 0) {
			gradientPerpendicularToLine = verticalLineGradient;
		} else {
			gradientPerpendicularToLine = -1 / gradientLine;
		}

		pointOnLine_1 = endPoint + (startPoint- endPoint).normalized * distanceToPerpendicularLineFromEndpoint + new Vector2 (1, gradientPerpendicularToLine).normalized;
		pointOnLine_2 = endPoint + (startPoint- endPoint).normalized * distanceToPerpendicularLineFromEndpoint - new Vector2 (1, gradientPerpendicularToLine).normalized;


		previousPerpendiculatPOintOnLine = startPoint;
	}

	public Vector2 PerpendicularPointOnLine (Vector2 point){
		bool pointFound = false;
		if (point.y > starPoint.y) {
			if (point.y < endPoint.y) {
				pointFound = true;
			}
		} else {
			if (point.y > endPoint.y) {
				pointFound = true;
			}
		}

		if (point.x > starPoint.x) {
			if (point.x < endPoint.x) {
				pointFound = true;
			}
		} else {
			if (point.x > endPoint.x) {
				pointFound = true;
			}
		}
		if (pointFound) {
			previousPerpendiculatPOintOnLine = Vector3.Project ((point - starPoint), (endPoint - starPoint)) + (Vector3)starPoint;
			return previousPerpendiculatPOintOnLine;
		}
		return previousPerpendiculatPOintOnLine;
	}

	public  float DistanceFromPointToLine( Vector2 c)
	{
		float s1 = -endPoint.y + starPoint.y;
		float s2 = endPoint.x - starPoint.x;
		return Mathf.Abs((c.x - starPoint.x) * s1 + (c.y - starPoint.y) * s2) / Mathf.Sqrt(s1*s1 + s2*s2);
	}

	public bool GetSide(Vector2 p) {
		return (p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);
	}

	public void DrawGizmos () {
		Gizmos.DrawLine (pointOnLine_1, pointOnLine_2);

	}

}
