using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    private Text text;
    private GameManager manager;

    private void Awake() {
        text = GetComponent<Text>();
        manager = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Health: "+ manager.HealthValue.ToString();
    }
}
