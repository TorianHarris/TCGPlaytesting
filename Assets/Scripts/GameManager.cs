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
    public TMPro.TMP_Text logText;
    public Color32 Red;
    public Color32 Yellow;
    public Color32 Purple;
    public Color32 Green;
    public Color32 Blue;
    public Color32 White;
    public Color32 Gray;
    public Color32 Orange;
    public Color32 Black;
    public Color32 playerLog;
    public Color32 opponentLog;
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
        if (Instance != null && Instance != this)
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

    public static void log(string msg)
    {
        Instance.logText.text = $"{Instance.logText.text}<color=white>{msg}</color>\n";
    }
    public static void log(string msg, Color clr)
    {
        if (clr == null || ColorUtility.ToHtmlStringRGBA(clr) == "00000000") clr = Color.white;
        Instance.logText.text = $"{Instance.logText.text}<color=#{ColorUtility.ToHtmlStringRGBA(clr)}>{msg}</color>\n";
    }

}
