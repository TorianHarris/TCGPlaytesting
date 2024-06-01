using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeck", menuName = "ScriptableObjects/NewDeck", order = 1)]
public class Deck : ScriptableObject
{
    public Leader leader;
    public List<Card> cards = new List<Card>();
    public List<Card> extraDeck = new List<Card>();
}
