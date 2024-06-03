using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public GameObject go;
    //public KeyCode keyCode;
    public Deck playerDeck;
    public Deck opponentDeck;
    public GameObject cardframe;
    public GameObject leaderCardframe;
    public int startingHandSize = 3;
    public int startingShields = 0;
    public float cardZoomSize = 2;
    //public enum classColor { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }
    public string tokenTolder;
    public bool mainDeckSearchable = false;
    public bool extraDeckSearchable = true;
    public GameObject enhancedCardPanel;
    public CardManager enhanceCard;
    public Color32 Red;
    public Color32 Yellow;
    public Color32 Purple;
    public Color32 Green;
    public Color32 Blue;
    public Color32 White;
    public Color32 Gray;
    public Color32 Orange;
    public Color32 Black;

    public string unitName = "Unit";
    public string instantName = "Spell";
    public string flashName = "Quick Spell";
    public string permanentName = "Artifact";
    [Space(10)]
    public string reactionName = "Reaction";
    public string equipName = "Equip";
    [Space(10)]
    public string leaderName = "Leader";

    [HideInInspector]
    public GameObject itemBeingDragged;

    void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(keyCode))
        //    go.SetActive(!go.activeSelf);
    }

}
