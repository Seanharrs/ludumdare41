using UnityEngine;
using UnityEngine.UI;

public class MagicCardDisplay : MonoBehaviour
{
    [SerializeField]
    private MagicCard m_CardData;

    [SerializeField]
    private Text m_CardName;

    [SerializeField]
    private Text m_Description;

    [SerializeField]
    private Image m_CardImage;

    [SerializeField]
    private Text m_Cost;

    [SerializeField]
    private Text m_DamageBoost;

    [SerializeField]
    private Text m_SpeedBoost;

    [SerializeField]
    private Text m_RangeBoost;

    [SerializeField]
    private Text m_EffectLength;

    private void Awake()
    {
        if(!m_CardData)
            return;

        m_CardName.text = m_CardData.cardName;
        m_Description.text = m_CardData.description;
        m_CardImage.sprite = m_CardData.image;
        m_Cost.text = m_CardData.cost.ToString();

        ShowMultiplier(m_CardData.damageMultiplier, m_DamageBoost);
        ShowMultiplier(m_CardData.speedMultiplier, m_SpeedBoost);
        ShowMultiplier(m_CardData.rangeMultiplier, m_RangeBoost);
        
        m_EffectLength.text = m_CardData.effectLength.ToString();
    }

    private void ShowMultiplier(float multi, Text guiMulti)
    {
        if(multi != 0)
            guiMulti.text = multi.ToString();
        else
            guiMulti.gameObject.SetActive(false);
    }
}
