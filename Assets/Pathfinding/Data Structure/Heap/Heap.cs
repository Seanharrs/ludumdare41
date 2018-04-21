using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> : IEnumerable where T : IHeapItem<T>  {

	public List<T> items{ get; protected set;}
	public Action changeVisual;

	 
	public Heap (){
		items = new List<T> ();
	}

	public void Add(T item) {
		item.HeapIndex = items.Count;
		items.Add (item);

		int treeLength = (int)Mathf.Log (items.Count, 2);

		if (item.HeapIndex != 0) {
			if (item.HeapIndex % 2 == 0) {
				item.Position = items [(item.HeapIndex - 1) / 2].Position + (Vector3.right/treeLength   + Vector3.down).normalized * 3;
			} else {
				item.Position = items [(item.HeapIndex - 1) / 2].Position + (Vector3.left/treeLength  + Vector3.down).normalized * 3;
			}
		} 
		SortUp(item);


	}

	public T RemoveFirst() {
		T firstItem = items[0];
		items[0] = items[items.Count - 1];
		items[0].HeapIndex = 0;
		items [0].Position = Vector3.zero;
		items.RemoveAt (items.Count - 1);

		SortDown(items[0]);
		return firstItem;
	}

	public void UpdateItem(T item) {
		SortUp(item);
		SortDown (item);
	}

	public int Count {
		get {
			return items.Count;
		}
	}

	public bool Contains(T item) {
		return Equals(items[item.HeapIndex], item);
	}


	void SortDown(T item) {
		while (true) {
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < items.Count) {
				swapIndex = childIndexLeft;

				if (childIndexRight < items.Count) {
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo(items[swapIndex]) < 0) {
					Swap (item,items[swapIndex]);
				}
				else {
					return;
				}

			}
			else {
				return;
			}

		}
	}

	void SortUp(T item) {
		int parentIndex = (item.HeapIndex-1)/2;

		while (true) {
			T parentItem = items[parentIndex];
			if (item.CompareTo(parentItem) > 0) {
				Swap (item,parentItem);
			}
			else {
				break;
			}

			parentIndex = (item.HeapIndex-1)/2;
		}
	}

	void Swap(T itemA, T itemB) {
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;

		Vector3 tempPos = itemA.Position;
		itemA.Position = itemB.Position;
		itemB.Position = tempPos;
	}



	public IEnumerator GetEnumerator () {
		return new AIenumertor (this);
	}

	public class AIenumertor : IEnumerator{

		Heap<T> instance;
		int position = -1;

		public AIenumertor (Heap<T> inst){
			this.instance = inst;
		}

		public object Current {
			get	{
				try	{
					return instance.items[position];
				}
				catch (IndexOutOfRangeException) {
					throw new InvalidOperationException();
				}
			}
		}

		public bool MoveNext() {
			position++;
			return (position < instance.items.Count);
		}

		public void Reset ()
		{
			position = 0;
		}

	}

}

public interface IHeapItem<T> : IComparable<T>{
	int HeapIndex {
		get;
		set;
	}

	Vector3 Position {
		get;
		set;
	}
}
