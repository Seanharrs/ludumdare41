using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthValue : MonoBehaviour {

    public Text text;

    public GameManager manager;

    public GameObject GameManagerReference;

    GameManager gameManagerReference;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        text = GetComponent<Text>();
    }    

    // Use this for initialization
    void Start () {
        //gameManagerReference = gameManagerReference.GetComponent<GameManager>();
        //text.text = gameManagerReference.m_LivesLeft.ToString();
        text.text = "Values here";
    }
	
	// Update is called once per frame
	void Update () {
        //text.text = gameManagerReference.m_LivesLeft.ToString();

    }
}
