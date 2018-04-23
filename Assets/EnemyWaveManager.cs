using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWaveManager : MonoBehaviour
{
    public int WaveValue;

    [SerializeField]
    private string m_GameOverScene = "GameOver";

    private List<List<GameObject>> m_Waves;

    [SerializeField]
    private float m_SecondsBetweenWaves = 20f;

    [SerializeField]
    private Transform m_SpawnPoint;

    [SerializeField]
    private int m_MinSpawnOffset;

    [SerializeField]
    private int m_MaxSpawnOffset;

    [SerializeField]
    private int m_NumberOfWaves;

    [SerializeField]
    private GameObject[] m_EnemyTypes;

    [SerializeField]
    private Transform m_EnemyHolder;

    [SerializeField]
    [Tooltip("The bonus card currency provided per wave")]
    private int m_WaveBonus;

    private int m_WaveNum = 0;
    private bool m_WaveFinished = false;

    private void Awake()
    {
        m_Waves = new List<List<GameObject>>();

        System.Random rand = new System.Random();
        for(int i = 0; i < m_NumberOfWaves; ++i)
        {
            m_Waves.Add(new List<GameObject>());

            int extra = i + rand.Next(0, 3);
            int numEnemiesInWave = rand.Next(4 + extra, 5 + extra);
            for(int j = 0; j < numEnemiesInWave; ++j)
            {
                Vector2 spawnOffset = new Vector2(rand.Next(m_MinSpawnOffset, m_MaxSpawnOffset), 0);
                Vector2 pos = (Vector2)m_SpawnPoint.position - (spawnOffset * j);
                GameObject enemy = Instantiate(m_EnemyTypes[rand.Next(m_EnemyTypes.Length)], pos, Quaternion.identity) as GameObject;
                enemy.transform.parent = m_EnemyHolder;
                m_Waves[i].Add(enemy);
                m_Waves[i][j].SetActive(false);
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnWave", 0.01f, m_SecondsBetweenWaves);
    }

    private void SpawnWave()
    {
        if(m_WaveNum == m_NumberOfWaves)
        {
            m_WaveFinished = true;
            return;
        }

        for(int i = 0; i < m_Waves[m_WaveNum].Count; ++i)
            m_Waves[m_WaveNum][i].SetActive(true);

        CardController.instance.AddCurrency(m_WaveBonus);

        // New Cards
        CardController.instance.GiveNewCards();

        ++m_WaveNum;
    }

    void Update()
    {
        WaveValue = m_WaveNum;
        if(!m_WaveFinished)
        {
            return;
        }


        for(int i = 0; i < m_Waves.Count; i++)
        {
            for(int j = 0; j < m_Waves[i].Count; j++)
            {
                if(m_Waves[i][j].gameObject != null && m_Waves[i][j].gameObject.activeSelf)
                    return;
            }
        }

        // no move enemy so you win
        SceneManager.LoadScene(m_GameOverScene);

    }
}
