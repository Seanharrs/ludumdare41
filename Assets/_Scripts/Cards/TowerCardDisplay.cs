using UnityEngine;
using UnityEngine.UI;

public class TowerCardDisplay : MonoBehaviour, IDisplay
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

    public CardType type { get { return CardType.Tower; } }

    private GameObject tower;

    private void Awake()
    {
        if(!m_CardData)
            return;

        m_CardName.text = m_CardData.cardName;
        m_Description.text = m_CardData.description;
        m_CardImage.sprite = m_CardData.image;
        m_Cost.text = m_CardData.cost.ToString();
        m_Damage.text = m_CardData.damage.ToString();
        m_Range.text = m_CardData.range.ToString();
        m_ShootSpeed.text = m_CardData.shootSpeed.ToString();
	}

    public void SelectCard()
    {
//        tower.GetComponent<TowerAttack>().enabled = false;
//        tower.GetComponent<Collider2D>().enabled = false;
		CardController.Instance.SetCurrentSelectedCard (this);
    }

    public bool TryPlayCard(Vector2 pos)
    {
		tower = Instantiate(m_CardData.towerPrefab, pos, Quaternion.identity);
        tower.GetComponent<TowerAttack>().SetTowerValues(m_CardData.damage, m_CardData.range, m_CardData.shootSpeed);

        return true;
    }

	public Sprite GetCardVisual()
	{
		return m_CardData.image;
	}
}
