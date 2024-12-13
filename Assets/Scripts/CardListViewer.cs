using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardListViewer : MonoBehaviour
{
    public string baseFolder;
    public GameObject cardFrame;
    public Transform viewport;
    public List<Card> currentCards;
    public DeckListViewer deckListViewer;

    void Start()
    {
        createNewCardList("Red");
    }

    public void createNewCardList(string folder)
    {
        foreach (Transform child in viewport)
        {
            Destroy(child.gameObject);
            currentCards.Clear();
        }

        //List<Card> cardsToView;

        currentCards = Resources.LoadAll<Card>("Cards/" + baseFolder + "/" + folder).ToList();
        currentCards = currentCards.OrderBy(c => c.cost).ThenBy(c => c.type).ThenBy(c => c.name).ToList();
        foreach (Card card in currentCards)
        {
            GameObject newCard = card.createCard(cardFrame, viewport);
            newCard.transform.localScale = new Vector2(newCard.transform.localScale.x * 2, newCard.transform.localScale.y * 2);
            newCard.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            newCard.GetComponent<Button>().onClick.AddListener(delegate { deckListViewer.addCard(card); });
        }
    }
}
