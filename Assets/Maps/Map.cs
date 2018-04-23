using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeType{
	GROUND,
	WATER,
	TOWER,
	ROAD,
    TREE,
	NONE,
    OCCUPIED
}

public class Node {
	public int x;
	public int y;

	public float movementCost;
	public Node cameFrom;
	public float costSoFar;
	public bool isDiag;
	public NodeType NodeType;

	public Node (int x,int y){
		this.x = x;
		this.y = y;
		movementCost = -1;
		isDiag = false;
		NodeType = NodeType.NONE;
	}
}

public class Map : MonoBehaviour {

	public int _Width;
	public int _Height;

	public int gridSize = 1;
	public Transform mapTransform;

	public Regions regions;
	public bool showGizmos;

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

		width = Mathf.RoundToInt((_Width )/ gridSize);
		height = Mathf.RoundToInt((_Height )/ gridSize);

		if (maps!= null && maps.Length > 0) {
			System.Array.Clear (maps, 0, maps.Length);
		}
		maps = new Node[width, height];

		GenerateMap ();
	}

	void OnValidate () {
		
		width = Mathf.RoundToInt((_Width  )/ gridSize);
		height = Mathf.RoundToInt((_Height)/ gridSize);

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
						maps [x, y].NodeType = region.nodeType;
					}
				}

				// FIXME !! when we donot have collider then we just assume that it is Ground 
				if (maps [x, y].movementCost == -1) {
					maps [x, y].movementCost = 1;
					maps [x, y].NodeType = NodeType.GROUND;
				}
			}
		}
	}

	public Node GetNodeFromPosition (Vector3 pos){
		Vector3 worldPos = (pos ) + Vector3.right * gridSize  * ((width / 2)) + Vector3.up * gridSize * ((height / 2) );
		int x = (Mathf.RoundToInt(worldPos.x / gridSize));
		int y = Mathf.RoundToInt(worldPos.y / gridSize);
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

	public Vector3 GetPositionFromeNode (Node node)
	{
		int x = node.x;
		int y = node.y;

		Vector3 localPos = Vector3.right *( x * gridSize  - gridSize/2f) + Vector3.up *( y * gridSize  - gridSize/2f);

		Vector3 worldPos = localPos - Vector3.right * _Width / 2 - Vector3.up * _Height / 2;
		return worldPos;
	}

    public void SetNodeOccupied (Vector3 pos)
    {
        GetNodeFromPosition(pos).NodeType = NodeType.OCCUPIED;
    }

	public bool CanPlaceTower (Vector3 pos,int gridSizeX,int gridSizeY)
	{
		Node node = GetNodeFromPosition (pos);
		if (CanPlaceTower (gridSizeX, gridSizeY, node)) {
			return true;
		}
		return false;
	}

    public bool CanUseMagic (Vector3 pos,int gridSizeX,int gridSizeY)
    {
        Node node = GetNodeFromPosition(pos);
        if (node.NodeType == NodeType.OCCUPIED && CanUseCard (gridSizeX, gridSizeY, node, NodeType.OCCUPIED)) {
            return true;
        }
        return false;
    }

	// TODO here it should come tower so that it has it's own size and we can get it from ITowe ???
	bool CanPlaceTower (int gridSizeX,int gridSizeY,Node node) {

		// for now let say we can place tower in either ground or tower
		if (node.NodeType != NodeType.TOWER && node.NodeType != NodeType.GROUND) {
			return false;
		}

		if (node.NodeType == NodeType.TOWER) {
			if (CanUseCard (gridSizeX, gridSizeY, node, NodeType.TOWER)) {
				return true;
			}
		}

		if (node.NodeType == NodeType.GROUND) {
			if (CanUseCard (gridSizeX, gridSizeY, node, NodeType.GROUND)) {
				return true;
			}
		}

		return false;
	}

	bool CanUseCard (int gridSizeX,int gridSizeY,Node node,NodeType checkNode)
	{
		for (int y = node.y; y < node.y + gridSizeY; y++) {
			for (int x = node.x; x < node.x + gridSizeX; x++) {
				if (x < Width && y < Height) {
					Node nextNode = Nodes [x, y];
					if (nextNode.NodeType != checkNode) {
						return false;
					}
				} else {
					return true;
				}
			}
		}
		return true;
	}

	// For Debugging Purpose
	void OnDrawGizmos () {

		if (!showGizmos) {
			return;
		}

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

