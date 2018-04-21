using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Cards/Tower", order = 1)]
public class TowerCard : ScriptableObject, ICard, ITower
{
    [SerializeField]
    [Tooltip("The name of the tower")]
    private string m_CardName;
    public string cardName { get { return m_CardName; } }

    [SerializeField]
    [Tooltip("How much the tower costs to play")]
    private int m_Cost;
    public int cost { get { return m_Cost; } }

    [SerializeField]
    [Tooltip("The tower description")]
    private string m_Desc;
    public string description { get { return m_Desc; } }

    [SerializeField]
    [Tooltip("The tower artwork")]
    private Sprite m_Image;
    public Sprite image { get { return m_Image; } }

    [SerializeField]
    [Tooltip("The damage dealt per attack")]
    private int m_Dmg;
    public int damage { get { return m_Dmg; } }

    [SerializeField]
    [Tooltip("The firing range of the tower")]
    private int m_Range;
    public int range { get { return m_Range; } }

    [SerializeField]
    [Tooltip("The shooting speed of the tower, in shots per second")]
    private float m_ShootSpeed;
    public float shootSpeed { get { return m_ShootSpeed; } }
}
