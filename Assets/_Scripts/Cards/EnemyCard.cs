using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Cards/Enemy", order = 1)]
public class EnemyCard : ScriptableObject, ICard, IEnemy
{
    [SerializeField]
    [Tooltip("The name of the enemy")]
    private string m_CardName;
    public string cardName { get { return m_CardName; } }

    [SerializeField]
    [Tooltip("How much the enemy costs to play")]
    private int m_Cost;
    public int cost { get { return m_Cost; } }

    [SerializeField]
    [Tooltip("The enemy description")]
    private string m_Desc;
    public string description { get { return m_Desc; } }

    [SerializeField]
    [Tooltip("The enemy artwork")]
    private Sprite m_Image;
    public Sprite image { get { return m_Image; } }

    [SerializeField]
    [Tooltip("The health of the enemy")]
    private int m_Health;
    public int health { get { return m_Health; } }

    [SerializeField]
    [Tooltip("The movement speed of the enemy")]
    private int m_MoveSpeed;
    public int moveSpeed { get { return m_MoveSpeed; } }

    [SerializeField]
    [Tooltip("The prefab of the enemy to be placed")]
    private GameObject m_EnemyPrefab;
    public GameObject enemyPrefab { get { return m_EnemyPrefab; } }
}
