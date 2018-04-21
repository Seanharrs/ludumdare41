using UnityEngine;
using UnityEngine.UI;

public class CreepCardDisplay : MonoBehaviour
{
    [SerializeField]
    private CreepCard m_CardData;

    [SerializeField]
    private Text m_CardName;

    [SerializeField]
    private Text m_Description;

    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private Text m_Cost;

    [SerializeField]
    private Text m_Health;

    [SerializeField]
    private Text m_MoveSpeed;

    private void Awake()
    {
        m_CardName.text = m_CardData.cardName;
        m_Description.text = m_CardData.description;
        m_CardImage.sprite = m_CardData.image;
        m_Cost.text = m_CardData.cost.ToString();
        m_Health.text = m_CardData.health.ToString();
        m_MoveSpeed.text = m_CardData.moveSpeed.ToString();
    }
}
