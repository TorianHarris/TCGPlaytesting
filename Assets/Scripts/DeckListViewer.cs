using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class DeckListViewer : MonoBehaviour
{
    public Deck deck;
    public TMP_InputField textInput;
    public TMP_Text deckCount;
    public TMP_Text messager;
    public Button buttonPrefab;
    public Transform viewport;
    public string folderPath;
    public int maxCopies;
    public int maxCardsInDeck;
    public List<Card> viewingDeck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        loadDeck();
    }

    public void addCard(Card card)
    {
        // if not added to deck yet...
        if (!viewingDeck.Contains(card))
        {
            Button btn = Instantiate(buttonPrefab, viewport);
            btn.name = card.name;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = card.name;
            btn.GetComponent<Image>().color = card.getColor();
            btn.onClick.AddListener(delegate { removeCard(card); });
            viewingDeck.Add(card);
            deckCount.text = "Deck Count: " + viewingDeck.Count + "/" + maxCardsInDeck;
        }
        // else if less than 3 are in the deck...
        else if (viewingDeck.FindAll(c => c.name.Equals(card.name)).Count < maxCopies)
        {
            TMP_Text numInDeck = viewport.Find(card.name).GetChild(1).GetComponent<TMP_Text>();
            int num = int.Parse(numInDeck.text[1].ToString()) + 1;
            numInDeck.text = "x" + num;
            viewingDeck.Add(card);
            deckCount.text = "Deck Count: " + viewingDeck.Count + "/" + maxCardsInDeck;
        }
        else
            sendMessage("Can't add anymore " + card.name + " to the deck!");
    }

    public void removeCard(Card card)
    {
        if (viewingDeck.FindAll(c => c.name.Equals(card.name)).Count > 1)
        {
            TMP_Text numInDeck = viewport.Find(card.name).GetChild(1).GetComponent<TMP_Text>();
            int num = int.Parse(numInDeck.text[1].ToString()) - 1;
            numInDeck.text = "x" + num.ToString();
            viewingDeck.Remove(card);
            deckCount.text = "Deck Count: " + viewingDeck.Count + "/" + maxCardsInDeck;
        }
        else if (viewingDeck.FindAll(c => c.name.Equals(card.name)).Count == 1)
        {
            Destroy(viewport.Find(card.name).gameObject);
            viewingDeck.Remove(card);
            deckCount.text = "Deck Count: " + viewingDeck.Count + "/" + maxCardsInDeck;
        }
    }

    public void newDeck()
    {
        foreach (Transform child in viewport)
        {
            //needed to avoid timing issues with children not being destroyed immediately
            child.gameObject.name = "_SET_TO_DESTROY_";
            Destroy(child.gameObject);
        }
        viewingDeck.Clear();
        deckCount.text = "Deck Count: " + viewingDeck.Count + "/" + maxCardsInDeck;
        sendMessage("Cleared the deck!");
    }

    public void loadDeck()
    {

        if (deck)
        {
            newDeck();
            textInput.text = deck.name;
            foreach (Card card in deck.cards)
            {
                addCard(card);
            }
            //sendMessage("Loaded " + deck.name + ".");
        }
        //else
        //sendMessage("No deck to load!");

    }

    public void saveDeck()
    {
        if (viewingDeck.Count == maxCardsInDeck)
        {
            Deck deckToSave = ScriptableObject.CreateInstance<Deck>();
            deckToSave.name = textInput.text;
            deckToSave.cards.AddRange(viewingDeck);
            AssetDatabase.CreateAsset(deckToSave, folderPath + deckToSave.name + ".asset");
            sendMessage("Deck saved!");
        }
        else
            sendMessage("Deck incomplete! Cannot save.");
    }

    public void sendMessage(string message)
    {
        //messager.color = Color.white;
        messager.text = message;
        LeanTween.cancel(messager.gameObject);
        LeanTween.value(messager.gameObject, updateValueExampleCallback, messager.color, new Color(1f, 1f, 1f, 0f), 0.2f).setDelay(2f);
    }

    void updateValueExampleCallback(Color val)
    {
        messager.color = val;
    }
}
