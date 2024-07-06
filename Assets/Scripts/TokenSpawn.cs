using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;


public class TokenSpawn : MonoBehaviour
{
    public RectTransform content;
    public Button buttonPrefab;
    public Card[] cards;
    private Transform playArea;

    void Start()
    {
        cards = Resources.LoadAll<Card>(GameManager.Instance.tokenFolder);
        playArea = GameObject.FindGameObjectWithTag("Play Area").transform;
        foreach (var card in cards)
        {
            Button btn = Instantiate(buttonPrefab, content.transform);
            btn.GetComponentInChildren<TMP_Text>().text = card.name;
            btn.onClick.AddListener(delegate { create(card); });
        }
    }

    public void create(Card card)
    {
        card.createCard(GameManager.Instance.cardframe, playArea);
        GameManager.log(card.name + " Token was Created");
    }
}
