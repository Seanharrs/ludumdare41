using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private List<List<GameObject>> m_Waves;

    [SerializeField]
    private Transform m_SpawnPoint;

    [SerializeField]
    private Vector2 m_SpawnOffset;

    [SerializeField]
    private int m_NumberOfWaves;

    [SerializeField]
    private GameObject[] m_EnemyTypes;

    [SerializeField]
    private Transform m_EnemyHolder;

    private float m_TimeBetweenWaves = 20f; //seconds

    private int m_WaveNum = 0;

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
                Vector2 pos = (Vector2)m_SpawnPoint.position - (m_SpawnOffset * j);
                GameObject enemy = Instantiate(m_EnemyTypes[rand.Next(m_EnemyTypes.Length)], pos, Quaternion.identity) as GameObject;
                enemy.transform.parent = m_EnemyHolder;
                m_Waves[i].Add(enemy);
                m_Waves[i][j].SetActive(false);
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnWave", 0.01f, m_TimeBetweenWaves);
    }

    private void SpawnWave()
    {
        for(int i = 0; i < m_Waves[m_WaveNum].Count; ++i)
            m_Waves[m_WaveNum][i].SetActive(true);

        ++m_WaveNum;
    }
}
