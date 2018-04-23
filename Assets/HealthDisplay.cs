using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    private Text text;

    private void Awake() { text = GetComponent<Text>(); }

    // Use this for initialization
    void Start () {
        text.text = "69";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
