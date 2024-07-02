using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/NewCard", order = 1)]
public class Card : ScriptableObject
{
    public enum cardType { Unit, Instant, Flash, Permanent, Reaction, Equip }
    public enum classColor { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }

    //public string cardName;
    public int cost;
    public int power;
    public int health;
    public cardType type;
    public classColor color;
    [TextArea]
    public string mainEffect;
    //[TextArea]
    public string secondaryEffect;
    public string trait;
    public Sprite cardArt;

    [HideInInspector]
    public bool exposed = false;

    public Color getColor()
    {
        switch (color)
        {
            case classColor.Red:
                return GameManager.Instance.Red;
            case classColor.Yellow:
                return GameManager.Instance.Yellow;
            case classColor.Purple:
                return GameManager.Instance.Purple;
            case classColor.Green:
                return GameManager.Instance.Green;
            case classColor.Blue:
                return GameManager.Instance.Blue;
            case classColor.White:
                return GameManager.Instance.White;
            case classColor.Gray:
                return GameManager.Instance.Gray;
            case classColor.Orange:
                return GameManager.Instance.Orange;
            case classColor.Black:
                return GameManager.Instance.Black;
            default:
                return Color.black;
        }
    }

    public string getTypeName()
    {
        switch (type)
        {
            case cardType.Unit:
                return GameManager.Instance.unitName;
            case cardType.Instant:
                return GameManager.Instance.instantName;
            case cardType.Flash:
                return GameManager.Instance.flashName;
            case cardType.Permanent:
                return GameManager.Instance.permanentName;
            case cardType.Reaction:
                return GameManager.Instance.reactionName;
            case cardType.Equip:
                return GameManager.Instance.equipName;
            default:
                return "CardType error";
        }
    }

    public void changeEnhancedCard()
    {
        GameManager.Instance.enhanceCard.GetComponent<CardManager>().FillCardInfo(name, cost, power, health, type, getTypeName(), getColor(), mainEffect, secondaryEffect, cardArt, trait, this);
        GameManager.Instance.enhanceCard.name = name;
    }

    public GameObject createCard(GameObject c, Transform t)
    {
        GameObject card = Instantiate(c, t);
        card.GetComponent<CardManager>().FillCardInfo(name, cost, power, health, type, getTypeName(), getColor(), mainEffect, secondaryEffect, cardArt, trait, this);
        card.name = name;
        return card;
    }

    public void createCard(GameObject c, Transform t, DeckManager d, bool mainDeck)
    {
        GameObject card = Instantiate(c, t);
        card.GetComponent<CardManager>().FillCardInfo(name, cost, power, health, type, getTypeName(), getColor(), mainEffect, secondaryEffect, cardArt, trait, this, d, mainDeck);
        card.name = name;
    }

    public void createCard(GameObject c, Transform t, DeckManager d, Vector2 pos, bool mainDeck)
    {
        GameObject card = Instantiate(c, t);
        card.GetComponent<CardManager>().FillCardInfo(name, cost, power, health, type, getTypeName(), getColor(), mainEffect, secondaryEffect, cardArt, trait, this, d, mainDeck);
        card.name = name;
        card.transform.position = pos;
    }

}
