using UnityEngine;
using UnityEngine.UI;

public class TowerCardDisplay : MonoBehaviour
{
    [SerializeField]
    private TowerCard m_CardData;

    [SerializeField]
    private Text m_CardName;

    [SerializeField]
    private Text m_Description;

    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private Text m_Cost;

    [SerializeField]
    private Text m_Damage;

    [SerializeField]
    private Text m_Range;

    [SerializeField]
    private Text m_ShootSpeed;

    private void Awake()
    {
        m_CardName.text = m_CardData.cardName;
        m_Description.text = m_CardData.description;
        m_CardImage.sprite = m_CardData.image;
        m_Cost.text = m_CardData.cost.ToString();
        m_Damage.text = m_CardData.damage.ToString();
        m_Range.text = m_CardData.range.ToString();
        m_ShootSpeed.text = m_CardData.shootSpeed.ToString();
	}
}
