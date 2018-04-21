using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Example script for placeing the tower to it's position
public class PlayerInput : MonoBehaviour {

	public SpriteRenderer towerPlacementVisual;
	public Transform towerPlacementTransfrom;

	Map map;


	void Start () {
		map = GameObject.FindObjectOfType<Map> ();
	}
	
	void Update () {

		if (Input.GetMouseButton (0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;

			if (map.PlaceTower (pos,1,1)) {
				towerPlacementVisual.color = Color.red;
			} else {
				towerPlacementVisual.color = Color.black;
			}
			pos.x =  Mathf.RoundToInt(pos.x);
			pos.y = Mathf.RoundToInt(pos.y);
			towerPlacementTransfrom.transform.localPosition = pos;
			towerPlacementTransfrom.transform.localScale = Vector3.one ;

		}
		
	}
		


}
