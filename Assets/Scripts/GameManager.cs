using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public KeyCode logKeyCode = KeyCode.L;
    [HideInInspector]
    public KeyCode diceKeyCode = KeyCode.R;

    [HideInInspector]
    public string heldMsg = "";
    [HideInInspector]
    public int originalHeldNum;
    [HideInInspector]
    public int changedHeldNum;
    [HideInInspector]
    public bool holding;
    [HideInInspector]
    public float holdingTimer;
    public float timeTilRelease;
    public Deck playerDeck;
    public Deck opponentDeck;
    public int startingHandSize = 3;
    public int startingShields = 0;
    public string tokenFolder;
    [Space(10)]
    public string unitName = "Unit";
    public string instantName = "Spell";
    public string permanentName = "Artifact";
    public string leaderName = "Leader";
    public string flashName = "Quick Spell";
    public string reactionName = "Reaction";
    public string equipName = "Equip";

    [HideInInspector]
    public GameObject itemBeingDragged;
    [Space(10)]
    public GameObject logger;
    [HideInInspector]
    public TMPro.TMP_Text logText;
    public GameObject magnifier;
    [HideInInspector]
    public GameObject magnifiedCard;
    [HideInInspector]
    public GameObject magnifiedLeaderCard;
    public ColorPalette palette;
    [HideInInspector]
    public Color32 Red;
    [HideInInspector]
    public Color32 Yellow;
    [HideInInspector]
    public Color32 Purple;
    [HideInInspector]
    public Color32 Green;
    [HideInInspector]
    public Color32 Blue;
    [HideInInspector]
    public Color32 White;
    [HideInInspector]
    public Color32 Gray;
    [HideInInspector]
    public Color32 Orange;
    [HideInInspector]
    public Color32 Black;
    [HideInInspector]
    public Color32 playerLogColor;
    [HideInInspector]
    public Color32 opponentLogColor;
    public GameObject cardframe;
    public GameObject leaderCardframe;
    [Space(10)]
    public List<CardZone> playerCardZones;
    public List<CardZone> opponentCardZones;

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
        logText = logger.GetComponentInChildren<TMP_Text>();

        magnifiedCard = Instantiate(cardframe, magnifier.transform);
        magnifiedCard.transform.localScale = new Vector3(3, 3);
        magnifiedCard.GetComponent<Button>().enabled = false;
        magnifiedCard.GetComponent<CardScript>().enabled = false;
        magnifiedCard.SetActive(false);

        if (leaderCardframe)
        {
            magnifiedLeaderCard = Instantiate(leaderCardframe, magnifier.transform);
            magnifiedLeaderCard.transform.localScale = new Vector3(3, 3);
            magnifiedLeaderCard.GetComponent<Button>().enabled = false;
            magnifiedLeaderCard.SetActive(false);
        }


        Red = palette.Red;
        Yellow = palette.Yellow;
        Purple = palette.Purple;
        Green = palette.Green;
        Blue = palette.Blue;
        White = palette.White;
        Gray = palette.Gray;
        Orange = palette.Orange;
        Black = palette.Black;
        playerLogColor = palette.playerLogColor;
        opponentLogColor = palette.opponentLogColor;
    }

    void Update()
    {
        // if (holding)
        // {
        //     holdingTimer += Time.deltaTime;

        //     if (holdingTimer >= timeTilRelease)
        //     {
        //         releaseHeldMsg();
        //     }
        // }
        // else
        // {
        //     holdingTimer = 0f;
        // }
        if (Input.GetKeyDown(logKeyCode))
            logger.SetActive(!logger.activeSelf);
        if (Input.GetKeyDown(diceKeyCode))
        {
            int randomNumber = Random.Range(1, 7);
            log("Dice Roll: " + randomNumber);
        }

    }
    public void ActiveAllPlayerZones()
    {
        {
            foreach (CardZone zone in playerCardZones)
            {
                zone.ActiveAllCards();
            }
        }
    }

    public void ActiveAllOpponentZones()
    {
        {
            foreach (CardZone zone in opponentCardZones)
            {
                zone.ActiveAllCards();
            }
        }
    }

    public static void magnifierToggle()
    {
        Instance.magnifier.SetActive(false);
        Instance.magnifiedCard.SetActive(false);
        if (Instance.leaderCardframe) Instance.magnifiedLeaderCard.SetActive(false);
    }

    public static void log(string msg)
    {
        if (Instance.heldMsg != "") releaseHeldMsg();
        Instance.logText.text = $"{Instance.logText.text}<color=white>{msg}</color>\n";
    }
    public static void log(string msg, bool mainPlayer)
    {
        if (Instance.heldMsg != "") releaseHeldMsg();
        Color clr = mainPlayer ? Instance.playerLogColor : Instance.opponentLogColor;
        Instance.logText.text = $"{Instance.logText.text}<color=#{ColorUtility.ToHtmlStringRGBA(clr)}>{msg}</color>\n";
    }
    public static void holdLog(string msg, int originalNum, int changedNum)
    {
        if (Instance.heldMsg != msg)
        {
            if (Instance.heldMsg != "") releaseHeldMsg();
            Instance.heldMsg = msg;
            Instance.originalHeldNum = originalNum;
            Instance.changedHeldNum = changedNum;
        }
        else
        {
            //Instance.holdingTimer = 0f;
            Instance.changedHeldNum = changedNum;
        }
    }

    public static void releaseHeldMsg()
    {
        string additionalMsg = $"{Instance.originalHeldNum} -> {Instance.changedHeldNum}";
        Instance.logText.text = $"{Instance.logText.text}<color=white>{Instance.heldMsg + additionalMsg}</color>\n";
        Instance.heldMsg = "";
        Instance.originalHeldNum = 0;
        Instance.changedHeldNum = 0;
        //Instance.holding = false;
    }

    // public static void holdLog(string msg, int num, Color clr)
    // {
    //     if (Instance.heldLog != "" && Instance.heldLog != msg)
    //     {
    //         Instance.logText.text = $"{Instance.logText.text}<color=white>{Instance.heldLog}</color>\n";
    //     }
    //     Instance.logText.text = $"{Instance.logText.text}<color=white>{msg}</color>\n";
    // }


}
