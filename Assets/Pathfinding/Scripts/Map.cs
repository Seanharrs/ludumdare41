using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public int gridSize = 1;
	public Transform mapTransform;

	public Regions regions;

	Node[,] maps;
	int width;
	int height;

	public Node[,] Nodes
	{
		get{
			return maps;
		}
	}

	public int Width
	{
		get{
			return width;
		}
	}

	public int Height
	{
		get{
			return height;
		}
	}

	void Start () {

		width = Mathf.RoundToInt((mapTransform.localScale.x )/ gridSize);
		height = Mathf.RoundToInt((mapTransform.localScale.y )/ gridSize);

		if (maps!= null && maps.Length > 0) {
			System.Array.Clear (maps, 0, maps.Length);
		}
		maps = new Node[width, height];

		GenerateMap ();
	}

	void OnValidate () {
		
		width = Mathf.RoundToInt((mapTransform.localScale.x  )/ gridSize);
		height = Mathf.RoundToInt((mapTransform.localScale.y )/ gridSize);

		if (maps!= null && maps.Length > 0) {
			System.Array.Clear (maps, 0, maps.Length);
		}
		maps = new Node[width, height];

		GenerateMap ();
	}

	void GenerateMap () {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				Vector3 worldPos = mapTransform.position - Vector3.right * gridSize  * (width / 2 - x  - 0.5f )  - Vector3.up * gridSize * (height / 2 - y  - 0.5f );
				maps [x, y] = new Node (x, y);

				foreach (Region region in regions.regions) {
					if (Physics2D.OverlapCircle (worldPos, gridSize/2f, region.regionMask)) {
						maps [x, y].movementCost = region.movementCost;
					}
				}

				//FIXME !! for now it is road but then we are going to have tile hence we will add collider to it so it will also get detected
				// it it does't collide to any thing then it must nothing or it much not be tranversable hence assigning it to infinity
				if (maps [x, y].movementCost == -1) {
					maps [x, y].movementCost = 1;
				}
			}
		}
	}

	public Node GetNodeFromPosition (Vector3 pos){
		Vector3 worldPos = (pos ) + Vector3.right * gridSize  * ((width / 2)) + Vector3.up * gridSize * ((height / 2) );
		int x = ((int)(worldPos.x / gridSize));
		int y = (int)(worldPos.y / gridSize);
		if (x >= width -1) {
			x = width - 1;
		}

		if (x < 0) {
			x = 0;
		}

		if (y < 0) {
			y = 0;
		}

		if (y >= height - 1) {
			y = height - 1;
		}
		return maps [ x,y];
	}

	// For Debugging Purpose
	void OnDrawGizmos () {
		if (maps != null) {
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					Vector3 worldPos = mapTransform.position - Vector3.right * gridSize  * (width / 2 - x  - 0.5f )  - Vector3.up * gridSize * (height / 2 - y  - 0.5f );
					Gizmos.color = (maps [x, y].movementCost == 0) ? Color.black : Color.red;
				
					foreach (Region region in regions.regions) {
						if (maps [x, y].movementCost == region.movementCost) {
							Gizmos.color = region.gizmosColor;
							Gizmos.DrawCube (worldPos, Vector3.one * gridSize * 0.5f);
						}
					}
				}
			}
		}
	}
	

}

