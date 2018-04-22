using UnityEngine;
using UnityEngine.UI;

public class MagicCardDisplay : MonoBehaviour, IDisplay
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

    public CardType type { get { return CardType.Magic; } }

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

    public void SelectCard()
    {
        foreach(TowerAttack tower in FindObjectsOfType<TowerAttack>())
            if(tower.canUpgrade) tower.Highlight();
    }

    public bool TryPlayCard(Vector2 pos)
    {
		RaycastHit2D hit = Physics2D.Raycast(pos +Vector2.up * 5, Vector3.up,10);
        if(!hit.collider)
            return false;

        TowerAttack tower = hit.collider.gameObject.GetComponent<TowerAttack>();
        if(!tower)
            return false;

        bool success = tower.TryUpgradeTower(m_CardData.damageMultiplier, m_CardData.speedMultiplier, m_CardData.rangeMultiplier);
        if(success)
            foreach(TowerAttack t in FindObjectsOfType<TowerAttack>())
                t.RemoveHighlight();

        return success;
    }

	public Sprite GetTowerVisual ()
	{
		return m_CardData.image;
	}
}
