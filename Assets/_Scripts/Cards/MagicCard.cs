using UnityEngine;

[CreateAssetMenu(fileName = "New Magic", menuName = "Cards/Magic", order = 1)]
public class MagicCard : ScriptableObject, ICard, IMagic
{
    [SerializeField]
    [Tooltip("The name of the magic")]
    private string m_CardName;
    public string cardName { get { return m_CardName; } }

    [SerializeField]
    [Tooltip("How much the magic costs to play")]
    private int m_Cost;
    public int cost { get { return m_Cost; } }

    [SerializeField]
    [Tooltip("The magic description")]
    private string m_Desc;
    public string description { get { return m_Desc; } }

    [SerializeField]
    [Tooltip("The magic artwork")]
    private Sprite m_Image;
    public Sprite image { get { return m_Image; } }

    [SerializeField]
    [Tooltip("The damage multiplier the magic provides, 0 if none")]
    private float m_DamageMultiplier;
    public float damageMultiplier { get { return m_DamageMultiplier; } }

    [SerializeField]
    [Tooltip("The speed multiplier the magic provides, 0 if none")]
    private float m_SpeedMultiplier;
    public float speedMultiplier { get { return m_SpeedMultiplier; } }

    [SerializeField]
    [Tooltip("The range multiplier the magic provides, 0 if none")]
    private float m_RangeMultiplier;
    public float rangeMultiplier { get { return m_RangeMultiplier; } }

    [SerializeField]
    [Tooltip("How long the magic lasts for, in seconds")]
    private int m_Time;
    public int effectLength { get { return m_Time; } }
}
