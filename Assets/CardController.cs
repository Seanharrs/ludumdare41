using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardController : MonoBehaviour {

	public static CardController Instance;

	public IDisplay currentSelectedCard;
	Animator animator;

	bool show = false;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
		animator = GetComponent<Animator> ();
	}

	public void SetCurrentSelectedCard(IDisplay currentSelectedCard)
	{
		this.currentSelectedCard = currentSelectedCard;
	}

	public void ShowCard ()
	{
		show = !show;
		animator.SetTrigger ((show == true) ? "In" : "Out");
	}

}
