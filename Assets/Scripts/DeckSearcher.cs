using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Linq;

public class DeckSearcher : MonoBehaviour
{
    public DeckManager dm;
    public Button buttonPrefab;
    public GameObject deckHeaderPrefab;
    public Transform viewport;
    private Transform playArea;

    void Start()
    {
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    public void GetDeckContents()
    {
        if (dm.cards.Count > 0) CreateDeckView(dm.cards, true);
        if (dm.extraDeckCards.Count > 0) CreateDeckView(dm.extraDeckCards, false);
    }

    public void CreateDeckView(List<Card> cardPool, bool mainDeck)
    {
        List<Card> cards = new List<Card>();
        cards.AddRange(cardPool.OrderBy(x => x.name).ToList());
        GameObject dh = Instantiate(deckHeaderPrefab, viewport);
        dh.GetComponentInChildren<TMP_Text>().text = mainDeck ? "Main Deck" : "Extra Deck";

        foreach (Card card in cards)
        {
            if (!viewport.Find(card.name))
            {
                Button btn = Instantiate(buttonPrefab, viewport);
                btn.name = card.name;
                btn.transform.GetChild(0).GetComponent<TMP_Text>().text = card.name;

                bool isSearchable = mainDeck ? GameManager.Instance.mainDeckSearchable : GameManager.Instance.extraDeckSearchable;
                if (isSearchable) btn.onClick.AddListener(delegate { create(card, mainDeck, mainDeck ? dm.cards : dm.extraDeckCards); });
            }
            else
            {
                TMP_Text numInDeck = viewport.Find(card.name).GetChild(1).GetComponent<TMP_Text>();
                int num = int.Parse(numInDeck.text[1].ToString()) + 1;
                numInDeck.text = "x" + num;
            }
        }
    }

    public void RemoveContents()
    {
        foreach (Transform child in viewport)
        {
            Destroy(child.gameObject);
        }
    }

    public void create(Card card, bool mainDeck, List<Card> deck)
    {
        card.createCard(GameManager.Instance.cardframe, playArea, dm, mainDeck);
        deck.Remove(card);
        gameObject.SetActive(!gameObject.activeSelf);
        RemoveContents();
    }
}
