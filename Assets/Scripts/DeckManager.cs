using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool mainPlayer;
    public GameObject hand;
    public GameObject shield;
    public GameObject leaderZone;
    public Text deckCount;

    public Transform damageArea;
    public GameObject deckViewer;

    public List<Card> cardLibrary;
    public List<Card> cards;
    public List<Card> extraDeckCards;

    private Deck deck;
    private GameObject cardFrame;
    private GameObject leaderCardFrame;
    private int turnCount;
    private bool mouseOver = false;

    public void Start()
    {
        deck = mainPlayer ? deck = GameManager.Instance.playerDeck : GameManager.Instance.opponentDeck;
        cardFrame = GameManager.Instance.cardframe;
        leaderCardFrame = GameManager.Instance.leaderCardframe;
        foreach (Card card in deck.cards)
        {
            Card createdCard = ScriptableObject.Instantiate(card);
            createdCard.name = createdCard.name.Replace("(Clone)", "").Trim();
            cardLibrary.Add(createdCard);
        }
        cards = cardLibrary;
        extraDeckCards.AddRange(deck.extraDeck);
        shuffle();
        //draw starting hand
        if (deck.leader)
        {
            for (int i = deck.leader.LeaderLevels.Length - 1; i >= 0; i--)
            {
                deck.leader.LeaderLevels[i].createLeader(leaderCardFrame, leaderZone.transform);
            }
        }

        for (int i = 0; i < GameManager.Instance.startingHandSize; i++)
        {
            Draw();
        }

        for (int i = 0; i < GameManager.Instance.startingShields; i++)
        {
            CreateShield();
        }

        if (mainPlayer) turnCount = 1;
    }

    public void Draw()
    {
        GameManager.log((mainPlayer ? "Player" : "Opponent") + " drew " + cards[0].name, mainPlayer);
        cards[0].createCard(cardFrame, hand.transform, this, true);
        cards.RemoveAt(0);
        exposedCheck();
    }

    public void CreateShield()
    {
        cards[0].createCard(cardFrame, shield.transform, this, true);
        cards.RemoveAt(0);
        exposedCheck();
    }

    public void AddToDamageArea()
    {
        cards[0].createCard(cardFrame, damageArea, this, true);
        cards.RemoveAt(0);
        exposedCheck();
    }

    void Update()
    {
        deckCount.text = cards.Count.ToString();

        if (Input.GetKeyDown(KeyCode.Space) && mouseOver)
        {
            deckViewer.SetActive(!deckViewer.activeSelf);
            if (deckViewer.activeSelf) deckViewer.GetComponent<DeckSearcher>().GetDeckContents();
            else deckViewer.GetComponent<DeckSearcher>().RemoveContents();
        }

        if (Input.GetKeyDown(KeyCode.S) && mouseOver)
        {
            shuffle();
            GameManager.log((mainPlayer ? "Player" : "Opponent") + " shuffled their deck", mainPlayer);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && mouseOver)
        {
            shuffleTopX(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && mouseOver)
        {
            shuffleTopX(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && mouseOver)
        {
            shuffleTopX(4);
        }
    }

    public void shuffle()
    {
        cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    public void shuffleTopX(int num)
    {
        List<Card> firstPart = cards.Take(num).ToList();
        List<Card> secondPart = cards.Skip(num).ToList();
        firstPart = firstPart.OrderBy(x => UnityEngine.Random.value).ToList();
        firstPart.AddRange(secondPart);
        cards = firstPart;
        print("shuffled top " + num);
    }

    // public void backToDeck(string c, bool exposed)
    // {
    //     foreach (Card item in cardLibrary)
    //     {
    //         if (item.name.Equals(c))
    //         {
    //             Card createdCard = ScriptableObject.Instantiate(item);
    //             createdCard.name = createdCard.name.Replace("(Clone)", "").Trim();
    //             createdCard.exposed = exposed;
    //             cards.Add(createdCard);
    //             shuffle();
    //             exposedCheck();
    //             return;
    //         }
    //     }
    // }

    public void backToDeck(Card c, bool mainDeck, bool exposed)
    {
        (mainDeck ? cards : extraDeckCards).Add(c);
        shuffle();
        exposedCheck();
        return;
    }

    public void topOfDeck(Card c, bool mainDeck, bool exposed)
    {
        c.exposed = exposed;
        (mainDeck ? cards : extraDeckCards).Insert(0, c);
        exposedCheck();
        return;
    }

    public void bottomOfDeck(Card c, bool mainDeck, bool exposed)
    {
        c.exposed = exposed;
        (mainDeck ? cards : extraDeckCards).Add(c);
        exposedCheck();
        return;
    }

    public void cloneCard(Card c, Vector2 pos)
    {
        c.createCard(cardFrame, GameObject.FindGameObjectWithTag("Play Area").transform, this, pos, true);
    }

    public void exposedCheck()
    {
        if (cards[0].exposed)
        {
            cards[0].createCard(cardFrame, transform, this, true);
            cards.RemoveAt(0);
            exposedCheck();
        }
        else
            return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            AddToDamageArea();
        }
    }

    public void turnCounter()
    {
        turnCount++;
        GameManager.log((mainPlayer ? "Player Turn Count: " : "Opponent Turn Count: ") + turnCount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
