using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LeaderManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text cardNameText;
    public TMP_Text levelText;
    public TMP_Text primaryValueText;
    public TMP_Text powerText;
    public TMP_Text healthText;
    public Outline secondaryCardColor;
    public TMP_Text typeText;
    public TMP_Text mainEffectText;
    public TMP_Text traitText;
    public Image cardArt;
    public GameObject faceDownCover;
    public GameObject iceOverlay;
    public Image nameFrame;
    public Image secondaryNameFrame;
    public Image[] toPrimaryColor;

    private Transform canvas;
    [HideInInspector]
    public Transform currentParent;
    [HideInInspector]
    public Transform newParent;

    public bool active = true;
    public bool mouseOver = false;
    public static GameObject itemBeingDragged;
    private CanvasGroup canvasGroup;
    private Leader.leaderLevel leader;
    public void FillLeaderInfo(Leader.leaderLevel l)
    {
        leader = l;
        Color32 primaryColor = leader.getColor(leader.primaryColor.ToString());
        Color32 secondaryColor = leader.secondaryColor.ToString() == "None" ? leader.getColor(leader.primaryColor.ToString()) : leader.getColor(leader.secondaryColor.ToString());

        if (cardNameText) cardNameText.text = leader.cardName;
        if (levelText) levelText.text = leader.level.ToString();
        if (primaryValueText) primaryValueText.text = leader.primaryValue.ToString();
        if (powerText) powerText.text = leader.power.ToString();
        if (healthText) healthText.text = leader.health.ToString();
        if (nameFrame) nameFrame.color = leader.getColor(leader.primaryColor.ToString());
        if (secondaryCardColor) secondaryCardColor.effectColor = secondaryColor;
        if (secondaryNameFrame) secondaryNameFrame.color = secondaryColor;
        if (typeText) typeText.text = GameManager.Instance.leaderName;
        if (mainEffectText) mainEffectText.text = leader.mainEffect.Replace("[", "<b>[").Replace("]", "]</b>");
        if (traitText) traitText.text = leader.trait;
        if (cardArt) cardArt.sprite = leader.cardArt;

        foreach (Image img in toPrimaryColor)
        {
            img.color = primaryColor;
        }
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Play Area").transform;
        canvasGroup = GetComponent<CanvasGroup>();
        currentParent = transform.parent;
    }

    public void Update()
    {
        // Face Down
        if (Input.GetKeyDown(KeyCode.F) && mouseOver)
        {
            faceDownCover.SetActive(!faceDownCover.activeSelf);
        }

        // Ice Overlay
        if (Input.GetKeyDown(KeyCode.I) && mouseOver)
        {
            iceOverlay.SetActive(!iceOverlay.activeSelf);
        }

        // Go to bottom
        if (Input.GetKeyDown(KeyCode.DownArrow) && mouseOver)
        {
            transform.SetAsFirstSibling();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.itemBeingDragged = gameObject;
        canvasGroup.blocksRaycasts = false;
        newParent = canvas;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position += (Vector3)eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (newParent == canvas)
        {
            transform.position = currentParent.position;
            transform.SetParent(currentParent);
        }
        else
            currentParent = newParent;
        GameManager.Instance.itemBeingDragged = null;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleActive();
        }
    }

    public void enhanceCard()
    {
        GameManager.Instance.magnifiedLeaderCard.GetComponent<LeaderManager>().FillLeaderInfo(leader);
        GameManager.Instance.magnifiedLeaderCard.SetActive(true);
        GameManager.Instance.magnifier.SetActive(true);
    }

    public void ToggleActive()
    {
        if (active)
            transform.Rotate(0, 0, -90);
        else
            transform.Rotate(0, 0, 90);
        active = !active;
    }
}
