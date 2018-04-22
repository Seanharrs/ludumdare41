using UnityEngine;

public class Deck : MonoBehaviour
{
    private IDisplay m_SelectedCard = null;
    public IDisplay selectedCard { get { return m_SelectedCard; } }

    public void SelectCard(GameObject card)
    {
        m_SelectedCard = card.GetComponent<IDisplay>();
        m_SelectedCard.SelectCard();
    }

    private void Update()
    {
        if(m_SelectedCard != null && Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool success = false;

            switch(m_SelectedCard.type)
            {
                case CardType.Tower:
                    success = m_SelectedCard.TryPlayCard(pos);
                    break;
                case CardType.Enemy:
                    success = m_SelectedCard.TryPlayCard(pos);
                    break;
                case CardType.Magic:
                    success = m_SelectedCard.TryPlayCard(pos);
                    break;
            }

            if(!success)
                return;

            m_SelectedCard = null;
        }
    }
}
