using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisual : MonoBehaviour {
	[HideInInspector]
	public NodeHeap node;
	void Awake () {
		node = new NodeHeap ();
		GetComponentInChildren<TextMesh> ().text = node.nodeValue.ToString ();
	}
}

public class NodeHeap : IHeapItem<NodeHeap> {

	public GameObject g;
	public float nodeValue;
	Vector3 position;

	public Vector3 Position {
		get { 
			return position;
		}
		set { 
			position = value;
		}
	}

	public NodeHeap () {
		nodeValue = Random.Range (0, 100);
	}

	int heapIndex;

	public int HeapIndex {
		get {
			return heapIndex;
		}
		set { 
			heapIndex = value;
		}
	}

	public int CompareTo (NodeHeap other) {
		int compare = nodeValue.CompareTo (other.nodeValue);
		if (compare == 0) {
			if (heapIndex < other.HeapIndex) {
				return -1;
			}
		}
		return -compare;
	}
}
