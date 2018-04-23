using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour {

    private Text text;
    public EnemyWaveManager WaveManagerScript;

    private void Awake()
    {
        text = GetComponent<Text>();
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Wave: " + WaveManagerScript.WaveValue.ToString();
    }
}
