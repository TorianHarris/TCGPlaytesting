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
    public Transform viewport;
    private Transform playArea;
    // Start is called before the first frame update
    void Start()
    {
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDeckContents(List<Card>cards, bool canCreateCards, List<Card> deck)
    {
        cards = cards.OrderBy(c => c.name).ToList();
        foreach (Card card in cards)
        {
            if(!viewport.Find(card.name))
            {
                Button btn = Instantiate(buttonPrefab, viewport);
                btn.name = card.name;
                btn.transform.GetChild(0).GetComponent<TMP_Text>().text = card.name;
                if (canCreateCards) btn.onClick.AddListener(delegate { create(card, deck); });
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

    public void create(Card card, List<Card> deck)
    {
        card.createCard(GameManager.Instance.cardframe, playArea, dm);
        deck.Remove(card);
        gameObject.SetActive(!gameObject.activeSelf);
        RemoveContents();
    }
}
