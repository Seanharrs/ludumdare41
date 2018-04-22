using UnityEngine;
using UnityEngine.UI;

public class EnemyCardDisplay : MonoBehaviour, IDisplay
{
    [SerializeField]
    private EnemyCard m_CardData;

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

    public CardType type { get { return CardType.Enemy; } }
    
    private GameObject enemy;

    private void Awake()
    {
        if(!m_CardData)
            return;

        m_CardName.text = m_CardData.cardName;
        m_Description.text = m_CardData.description;
        m_CardImage.sprite = m_CardData.image;
        m_Cost.text = m_CardData.cost.ToString();
        m_Health.text = m_CardData.health.ToString();
        m_MoveSpeed.text = m_CardData.moveSpeed.ToString();
    }

    public void SelectCard()
    {
        enemy = Instantiate(m_CardData.enemyPrefab);
        enemy.GetComponent<Enemy>().enabled = false;
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.AddComponent<FollowMouse>();
    }

    public bool TryPlayCard(Vector2 pos)
    {
        enemy.transform.position = pos;
        //enemy.GetComponent<Enemy>().enabled = true;
        enemy.GetComponent<Collider2D>().enabled = true;
        Destroy(enemy.GetComponent<FollowMouse>());

        return true;
    }
}
