using UnityEngine;

[CreateAssetMenu(fileName = "New Creep", menuName = "Cards/Creep", order = 1)]
public class CreepCard : ScriptableObject, ICard, ICreep
{
    [SerializeField]
    [Tooltip("The name of the creep")]
    private string m_CardName;
    public string cardName { get { return m_CardName; } }

    [SerializeField]
    [Tooltip("How much the creep costs to play")]
    private int m_Cost;
    public int cost { get { return m_Cost; } }

    [SerializeField]
    [Tooltip("The creep description")]
    private string m_Desc;
    public string description { get { return m_Desc; } }

    [SerializeField]
    [Tooltip("The creep artwork")]
    private Sprite m_Image;
    public Sprite image { get { return m_Image; } }

    [SerializeField]
    [Tooltip("The health of the creep")]
    private int m_Health;
    public int health { get { return m_Health; } }

    [SerializeField]
    [Tooltip("The movement speed of the creep")]
    private int m_MoveSpeed;
    public int moveSpeed { get { return m_MoveSpeed; } }
}
