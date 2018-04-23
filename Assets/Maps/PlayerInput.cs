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
		towerPlacementVisual.sprite = null;
	}
	
	void Update () {

		towerPlacementTransfrom.gameObject.SetActive (false);

		if (CardController.Instance.currentSelectedCard == null) {
			return;
		}

		towerPlacementTransfrom.gameObject.SetActive (true);
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			
		pos.z = 0;

		pos.x =  Mathf.RoundToInt(pos.x);
		pos.y = Mathf.RoundToInt(pos.y);
		towerPlacementTransfrom.transform.localPosition = pos;
		towerPlacementTransfrom.transform.localScale = Vector3.one ;

		if (towerPlacementVisual.sprite == null) {
			towerPlacementVisual.sprite = CardController.Instance.currentSelectedCard.GetTowerVisual ();
		}

        // Tower is black if it cannot be placed on a tile and white if it can be placed on a tile
		if (map.PlaceTower (pos,1,1)) {
			towerPlacementVisual.color = Color.white;
		} else {
			towerPlacementVisual.color = Color.black;
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			towerPlacementTransfrom.gameObject.SetActive (false);
			CardController.Instance.currentSelectedCard.TryPlayCard (pos);
			CardController.Instance.currentSelectedCard = null;
			towerPlacementVisual.sprite = null;
		}
		
	}
		


}
