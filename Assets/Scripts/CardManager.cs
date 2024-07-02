using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public TMP_Text cardNameText;
    public TMP_Text cardType;
    public TMP_Text costText;
    public TMP_Text healthText;
    public TMP_Text powerText;
    public TMP_Text mainEffectText;
    public TMP_Text secondaryEffectText;
    public TMP_Text traitText;
    public GameObject faceDownCover;
    public GameObject iceOverlay;
    public Image cardArt;
    public GameObject powerImage;
    public GameObject healthImage;
    public GameObject secondaryFrame;
    public string healthPrefix = "";
    public Image[] toPrimaryColor;
    [HideInInspector]
    public DeckManager deck;
    [HideInInspector]
    public Card cardReference;
    public bool isInMainDeck;



    //for card viewer
    public void FillCardInfo(string cardName, int cost, int power, int health, Card.cardType type, string typeName, Color color, string mainEffect, string secondaryEffect, Sprite art, string trait, Card reference)
    {
        if (cardNameText)
            cardNameText.text = cardName;
        if (costText)
            costText.text = cost.ToString();
        if (cardType)
            cardType.text = typeName;
        if (powerText)
            powerText.text = type == Card.cardType.Unit ? power.ToString() : "";
        if (healthText)
            healthText.text = healthPrefix + health.ToString();
        if (mainEffectText)
            mainEffectText.text = mainEffect.Replace("[", "<b>[").Replace("]", "]</b>").Replace("n/", "\n");
        if (secondaryEffectText)
            secondaryEffectText.text = secondaryEffect;
        if (cardArt)
            cardArt.sprite = art;
        if (traitText)
            traitText.text = trait;

        if (toPrimaryColor.Length != 0)
        {
            foreach (Image img in toPrimaryColor)
            {
                img.color = color;
            }
        }
        if (powerImage) powerImage.SetActive(type == Card.cardType.Unit);
        if (healthImage) healthImage.SetActive(type == Card.cardType.Unit);
        if (secondaryFrame) secondaryFrame.SetActive(!string.IsNullOrWhiteSpace(secondaryEffect));

        cardReference = reference;
    }

    //for cards in a deck
    public void FillCardInfo(string cardName, int cost, int power, int health, Card.cardType type, string typeName, Color color, string mainEffect, string secondaryEffect, Sprite art, string trait, Card reference, DeckManager d, bool mainDeck)
    {
        if (cardNameText)
            cardNameText.text = cardName;
        if (costText)
            costText.text = cost.ToString();
        if (cardType)
            cardType.text = typeName;
        if (powerText)
            powerText.text = type == Card.cardType.Unit ? power.ToString() : "";
        if (healthText)
            healthText.text = healthPrefix + health.ToString();
        if (mainEffectText)
            mainEffectText.text = mainEffect.Replace("[", "<b>[").Replace("]", "]</b>").Replace("n/", "\n");
        if (secondaryEffectText)
            secondaryEffectText.text = secondaryEffect;
        if (cardArt)
            cardArt.sprite = art;
        if (traitText)
            traitText.text = trait;
        deck = d;

        if (toPrimaryColor.Length != 0)
        {
            foreach (Image img in toPrimaryColor)
            {
                img.color = color;
            }
        }
        if (powerImage) powerImage.SetActive(type == Card.cardType.Unit);
        if (healthImage) healthImage.SetActive(type == Card.cardType.Unit);
        if (secondaryFrame) secondaryFrame.SetActive(!string.IsNullOrWhiteSpace(secondaryEffect));

        cardReference = reference;
        isInMainDeck = mainDeck;
    }
}
