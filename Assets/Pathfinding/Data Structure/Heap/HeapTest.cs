using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapTest : MonoBehaviour {

	public GameObject nodeprefab;

	Heap<NodeHeap> nodes;

	void Start () {
		nodes = new Heap<NodeHeap> ();

		GameObject nodeGo = (GameObject)Instantiate (nodeprefab);

		NodeHeap node = nodeGo.GetComponent<NodeVisual> ().node;
		node.g = nodeGo;


		nodes.Add (node);
		node.Position = Vector3.zero;

	}

	void Update () {



		if (Input.GetMouseButtonDown (0)) {
			Vector3 inputPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			inputPos.z = 0;

			RaycastHit2D hit = Physics2D.Raycast (inputPos, Vector3.zero, int.MaxValue);
			if (hit) {
				hit.transform.GetComponent<NodeVisual> ().node.nodeValue = Random.Range (0, 100);
				hit.transform.GetComponentInChildren<TextMesh> ().text = hit.transform.GetComponent<NodeVisual> ().node.nodeValue.ToString();
				nodes.UpdateItem (hit.transform.GetComponent<NodeVisual> ().node);
			} else {	
				GameObject nodeGo = (GameObject)Instantiate (nodeprefab);
				NodeHeap node = nodeGo.GetComponent<NodeVisual> ().node;
				node.g = nodeGo;
				nodes.Add (node);
			}

			foreach (NodeHeap n in nodes) {
				n.g.transform.position = n.Position;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			NodeHeap removingNode = nodes.RemoveFirst ();
			Destroy (removingNode.g);

			foreach (NodeHeap n in nodes) {
				n.g.transform.position = n.Position;
			}
		}


	}


}
