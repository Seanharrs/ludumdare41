using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class PathFinding {

	public static void DoPathFind_BreathFirstSearch (Node startNode,Node endNode,Node[,] maps) {

		// clear everything 
		foreach (Node n in maps) {
			n.cameFrom = null;
		}

		Queue<Node> nodes = new Queue<Node> ();
		nodes.Enqueue (startNode);

		while (nodes.Count > 0) {
			Node currentNode = nodes.Dequeue ();
			if (currentNode == endNode) {
				// we found the path break the loop !!!
				RetracePath(endNode,startNode);
				return;
			}

			Node[] neighbour = NeighbourNode (currentNode, maps);

			foreach (Node n in neighbour) {
				if (n == null) {
					continue;
				}
				if (n.cameFrom == null) {
					nodes.Enqueue (n);
					n.cameFrom = currentNode;

				}
			}
		}

		// if we reach her mean's we were unable to find path 
		Debug.LogError("Unable to find Path");

	}

	public static void DoPathFind_Dijkstra (Node startNode,Node endNode,Node[,] maps) {

		// clear everything 
		foreach (Node n in maps) {
			n.cameFrom = null;
		}

		SimplePriorityQueue<Node> nodes = new SimplePriorityQueue<Node> ();
		nodes.Enqueue (startNode,startNode.costSoFar);

		while (nodes.Count > 0) {
			Node currentNode = nodes.Dequeue ();
			if (currentNode == endNode) {
				// we found the path break the loop !!!
				RetracePath(endNode,startNode);
				return;
			}

			Node[] neighbour = NeighbourNode (currentNode, maps);

			foreach (Node n in neighbour) {
				float newCost = currentNode.costSoFar + ((n.movementCost == 0) ? 10000 : n.movementCost * ((n.isDiag) ? 1.414f : 1));
				if (n.cameFrom == null ||  newCost < n.costSoFar) {
					n.costSoFar = newCost;
					if (n.cameFrom != null) {
						if (nodes.Contains (n)) {
							nodes.UpdatePriority (n, n.costSoFar);
						} else {
							nodes.Enqueue (n, n.costSoFar);
						}
					} else {
						nodes.Enqueue (n, n.costSoFar);
					}
					n.cameFrom = currentNode;
				}
			}
		}

		// if we reach her mean's we were unable to find path 
		Debug.LogError("Unable to find Path");

	}

	public static List<Node> DoPathFind_AStar (Node startNode,Node endNode,Node[,] maps) {

		// clear everything 
		foreach (Node n in maps) {
			n.cameFrom = null;
		}

		SimplePriorityQueue<Node> nodes = new SimplePriorityQueue<Node> ();
		nodes.Enqueue (startNode,startNode.costSoFar);

		while (nodes.Count > 0) {
			Node currentNode = nodes.Dequeue ();
			if (currentNode == endNode) {
				// we found the path break the loop !!!
				return RetracePath(endNode,startNode);
			}

			Node[] neighbour = NeighbourNode (currentNode, maps);

			foreach (Node n in neighbour) {
				float newCost = currentNode.costSoFar + ((n.movementCost == 0) ? 10000 : n.movementCost * ((n.isDiag) ? 1.414f : 1));
				if (n.cameFrom == null ||  newCost < n.costSoFar) {
					n.costSoFar = newCost;
					float priority = n.costSoFar + HeuristicDistance (endNode, n);

					if (n.cameFrom != null) {
						if (nodes.Contains (n)) {
							nodes.UpdatePriority (n, priority);
						} else {
							nodes.Enqueue (n, priority);
						}
					} else {
						nodes.Enqueue (n, priority);
					}
					n.cameFrom = currentNode;
				}
			}
		}

		// if we reach her mean's we were unable to find path 
		Debug.LogError("Unable to find Path");
		return null;

	}

	static List<Node> RetracePath (Node endNode,Node startNode) {

		List<Node> path = new List<Node>();

		path.Add (endNode);
		Node cameFrom = endNode.cameFrom;
		while (cameFrom != startNode) {
			path.Add (cameFrom);
			cameFrom = cameFrom.cameFrom;
		}
		path.Add (startNode);

		path.Reverse ();
		return path;
	}

	static Node[] NeighbourNode (Node currentNode,Node[,] maps) {
		List<Node> neighbour = new List<Node> ();

		for (int x = currentNode.x - 1; x <= currentNode.x + 1; x++) {
			for (int y = currentNode.y - 1; y <= currentNode.y + 1; y++) {

				if (x < maps.GetLength (0) && y < maps.GetLength (1) && x >= 0 && y >= 0 ) {

					if (x == currentNode.x && y == currentNode.y) {
						continue;
					}
				
					if ((x == currentNode.x || y == currentNode.y)) {
						
						neighbour.Add (maps [x, y]);
						maps [x, y].isDiag = false;
					}

					if ((x != currentNode.x && y != currentNode.y)) {

						neighbour.Add (maps [x, y]);
						maps [x, y].isDiag = true;
					}
				}
			}
		}

		return neighbour.ToArray();
	}

	static float HeuristicDistance (Node a , Node b ){
		float dx = Mathf.Abs (a.x - b.x);
		float dy = Mathf.Abs (a.y - b.y);
		float sum = 0;
		if (dy > dx) {
			sum = dx * 1.414f + (dy - dx) ;
		} else {
			sum = dy * 1.414f + (dx - dy) ;
		}
		return sum;
	}
}

public class Node {
	public int x;
	public int y;

	public float movementCost;
	public Node cameFrom;
	public float costSoFar;
	public bool isDiag;

	public Node (int x,int y){
		this.x = x;
		this.y = y;
		movementCost = -1;
		isDiag = false;
	}
}
