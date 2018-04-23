using UnityEngine;


// Example script for placeing the tower to it's position
public class PlayerInput : MonoBehaviour
{

    public SpriteRenderer cardPlacementVisual;
    public Transform cardPlacementTransform;

    Map map;

    void Start()
    {
        map = FindObjectOfType<Map>();
        cardPlacementVisual.sprite = null;
    }

    void Update()
    {

        cardPlacementTransform.gameObject.SetActive(false);

		if(CardController.instance != null && CardController.instance.currentSelectedCard == null)
        {
            return;
        }

        if(Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Escape))
        {
            CardController.instance.currentSelectedCard = null;
        }

        PlayCard();
    }

    private void PlayCard()
    {
        cardPlacementTransform.gameObject.SetActive(true);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pos.z = 0;

		pos.x = Mathf.RoundToInt((pos.x )/map.gridSize) * map.gridSize;
		pos.y = Mathf.RoundToInt((pos.y )/map.gridSize) * map.gridSize;
		pos.x -= 0.1f;
		pos.y -= 0.1f;

        cardPlacementTransform.transform.localPosition = pos;
        cardPlacementTransform.transform.localScale = Vector3.one;

        if(cardPlacementVisual.sprite == null)
        {
            cardPlacementVisual.sprite = CardController.instance.currentSelectedCard.GetCardVisual();
        }

		if (Input.GetMouseButtonDown (1)) {
			CardController.instance.UseSelectedCard(false);
			cardPlacementVisual.sprite = null;
		}

		if (CardController.instance != null && CardController.instance.currentSelectedCard != null) {
			if ((CardController.instance.currentSelectedCard.type == CardType.Tower && !map.CanPlaceTower (pos, 1, 1))
			  || (CardController.instance.currentSelectedCard.type == CardType.Magic && !map.CanUseMagic (pos, 1, 1))) {
				//Darken image if card cannot be used on that tile
				cardPlacementVisual.color = Color.black;
				return;
			}
		}

        cardPlacementVisual.color = Color.white;

        if(Input.GetMouseButtonDown(0))
        {
			pos.x += map.gridSize/2f ;
			pos.y += map.gridSize/2f ;
            bool success = CardController.instance.currentSelectedCard.TryPlayCard(pos);

            if(!success)
                return;

            cardPlacementVisual.sprite = null;
            cardPlacementTransform.gameObject.SetActive(false);

            if(CardController.instance.currentSelectedCard.type == CardType.Tower)
                map.SetNodeOccupied(pos);

            CardController.instance.currentSelectedCard.DestroyCard();

            CardController.instance.UseSelectedCard();
        }
    }
}
