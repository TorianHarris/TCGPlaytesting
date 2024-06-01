using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViewer : MonoBehaviour
{
    public RectTransform content;
    public Button buttonPrefab;
    public CardManager cardFrame;
    public LayoutGroup lg;

    private Card[] cards;

    void Start()
    {
        cards = Resources.LoadAll<Card>("Cards");
        foreach (var card in cards)
        {
            Button btn = Instantiate(buttonPrefab, content.transform);
            btn.GetComponentInChildren<TMP_Text>().text = card.name;
            btn.onClick.AddListener(delegate { create(card); });
        }
    }

    public void create(Card card)
    {
        cardFrame.powerImage.SetActive(true);
        cardFrame.healthImage.SetActive(true);
        cardFrame.secondaryFrame.SetActive(true);
        cardFrame.FillCardInfo(card.name, card.cost, card.power, card.health, card.type, card.getTypeName(), card.getColor(), card.mainEffect, card.secondaryEffect, card.cardArt, card.trait, card);
        LayoutRebuilder.ForceRebuildLayoutImmediate(lg.GetComponent<RectTransform>());
    }
}
