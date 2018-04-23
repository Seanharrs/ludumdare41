using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	public static CardController instance;

    private IDisplay m_CurrentSelectedCard;
	public IDisplay currentSelectedCard
    {
        get { return m_CurrentSelectedCard; }
        set { m_CurrentSelectedCard = value; }
    }

    [SerializeField]
    private int m_CurrencyLeft = 10;
    public int currencyLeft { get { return m_CurrencyLeft; } }

    private CurrencyText m_CurrencyText;

	private Animator m_Anim;

	private bool m_ShowCards = false;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
        m_CurrencyText = FindObjectOfType<CurrencyText>();
		m_Anim = GetComponent<Animator> ();
	}

	public void ShowCard ()
	{
		m_ShowCards = !m_ShowCards;
		m_Anim.SetTrigger (m_ShowCards ? "In" : "Out");
	}

    public void UseSelectedCard()
    {
        m_CurrencyLeft -= currentSelectedCard.GetCardCost();
        m_CurrencyText.UpdateCurrency(m_CurrencyLeft);
        currentSelectedCard = null;
    }

    public void AddCurrency(int val)
    {
        m_CurrencyLeft += val;
        m_CurrencyText.UpdateCurrency(m_CurrencyLeft);
    }
}
