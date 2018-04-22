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
        tower = Instantiate(m_CardData.towerPrefab);
        tower.GetComponent<TowerAttack>().enabled = false;
        tower.GetComponent<Collider2D>().enabled = false;
        tower.AddComponent<FollowMouse>();
    }

    public bool TryPlayCard(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.one * 0.1f);
        if(hit.collider)
            return false;

        tower.transform.position = pos;
        tower.GetComponent<TowerAttack>().enabled = true;
        tower.GetComponent<Collider2D>().enabled = true;
        Destroy(tower.GetComponent<FollowMouse>());

        return true;
    }
}
