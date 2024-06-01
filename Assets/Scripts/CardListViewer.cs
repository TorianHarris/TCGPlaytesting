using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardListViewer : MonoBehaviour
{
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
        }

        List<Card> cardsToView;

        cardsToView = Resources.LoadAll<Card>("Cards/" + folder).ToList();
        cardsToView = cardsToView.OrderBy(c => c.cost).ThenBy(c => c.type).ThenBy(c => c.name).ToList();
        foreach (Card card in cardsToView)
        {
            GameObject newCard = card.createCard(cardFrame, viewport);
            newCard.GetComponent<Button>().onClick.AddListener(delegate { deckListViewer.addCard(card); });
        }
    }
}
