using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField]
    private int m_UpgradeLevel = 1;
    public int level
    {
        get { return m_UpgradeLevel; }
        set { m_UpgradeLevel += value; }
    }

    [SerializeField]
    private int m_MaxLevel = 3;
    public int maxLevel { get { return m_MaxLevel; } }

    [SerializeField]
    private float m_DmgBoost = 1.2f;
    public float damageBoost { get { return m_DmgBoost; } }

    [SerializeField]
    private float m_RangeBoost = 1.4f;
    public float rangeBoost { get { return m_RangeBoost; } }

    [SerializeField]
    private float m_SpeedBoost = 1.5f;
    public float speedBoost { get { return m_SpeedBoost; } }
}
