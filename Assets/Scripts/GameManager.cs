using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public KeyCode logKeyCode;
    public KeyCode diceKeyCode;
    public GameObject logger;
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
    public GameObject cardframe;
    public GameObject leaderCardframe;
    public int startingHandSize = 3;
    public int startingShields = 0;
    public float cardZoomSize = 2;
    //public enum classColor { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }
    public string tokenFolder;
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

    public static void log(string msg)
    {
        if (Instance.heldMsg != "") releaseHeldMsg();
        Instance.logText.text = $"{Instance.logText.text}<color=white>{msg}</color>\n";
    }
    public static void log(string msg, Color clr)
    {
        if (Instance.heldMsg != "") releaseHeldMsg();
        if (clr == null || ColorUtility.ToHtmlStringRGBA(clr) == "00000000") clr = Color.white;
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
        print("releaseing my message!");
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
