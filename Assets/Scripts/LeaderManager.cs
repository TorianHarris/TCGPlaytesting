using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderManager : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    private bool resized;
    private Vector3 origSize;

    public void FillLeaderInfo(string cardName, int level, int primaryValue, int power, int health, Color primaryColor, Color secondaryColor, string type, string mainEffect, string trait, Sprite art )
    {
        if (cardNameText) cardNameText.text = cardName;
        if (levelText) levelText.text = level.ToString();
        if (primaryValueText) primaryValueText.text = primaryValue.ToString();
        if (powerText) powerText.text = power.ToString();
        if (healthText) healthText.text = health.ToString();
        if (nameFrame) nameFrame.color = primaryColor;
        if (secondaryCardColor) secondaryCardColor.effectColor = secondaryColor;
        if (secondaryNameFrame) secondaryNameFrame.color = secondaryColor;
        if (typeText) typeText.text = type;
        if (mainEffectText) mainEffectText.text = mainEffect.Replace("[", "<b>[").Replace("]", "]</b>");
        if (traitText) traitText.text = trait;
        if (cardArt) cardArt.sprite = art;

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
        origSize = GetComponent<RectTransform>().localScale;
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

    public void Resize()
    {
        if (!resized)
        {
            GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x * 2, GetComponent<RectTransform>().localScale.y * 2, 1);
            resized = true;
            transform.SetParent(canvas);
            transform.SetAsLastSibling();
        }
        else
        {
            GetComponent<RectTransform>().localScale = origSize;
            resized = false;
            transform.SetParent(currentParent);
            transform.SetAsLastSibling();
        }
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
