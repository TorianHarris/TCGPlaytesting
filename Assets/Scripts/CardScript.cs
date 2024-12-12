using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDropHandler
{
    private Transform canvas;
    private CardManager cardManager;
    // [HideInInspector]
    // public GameObject deck;
    [HideInInspector]
    public Transform currentParent;
    [HideInInspector]
    public Transform newParent;

    public bool active = true;
    public bool mouseOver = false;

    [HideInInspector]
    public GameObject faceDownCover;
    public GameObject iceOverlay;
    //public static GameObject itemBeingDragged;
    private CanvasGroup canvasGroup;
    private bool exposed;
    private bool resized;
    private Vector3 origSize;
    private bool mainPlayersCard;

    public void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Play Area").transform;
        cardManager = gameObject.GetComponent<CardManager>();
        faceDownCover = cardManager.faceDownCover;
        iceOverlay = cardManager.iceOverlay;
        canvasGroup = GetComponent<CanvasGroup>();
        currentParent = transform.parent;
        origSize = GetComponent<RectTransform>().localScale;
        if (cardManager.deck) mainPlayersCard = cardManager.deck.mainPlayer;
    }

    public void Update()
    {
        // Shuffle into deck
        if (Input.GetKeyDown(KeyCode.S) && mouseOver)
        {
            GameManager.log(gameObject.name + " was shuffled into the deck", mainPlayersCard);
            cardManager.deck.backToDeck(cardManager.cardReference, cardManager.isInMainDeck, exposed);
            Destroy(gameObject);
        }

        // Face Down
        if (Input.GetKeyDown(KeyCode.F) && mouseOver)
        {
            GameManager.log(gameObject.name + " was placed facedown", mainPlayersCard);
            faceDownCover.SetActive(!faceDownCover.activeSelf);
        }

        // Ice Overlay
        if (Input.GetKeyDown(KeyCode.I) && mouseOver)
        {
            GameManager.log(gameObject.name + " was Frozen", mainPlayersCard);
            iceOverlay.SetActive(!iceOverlay.activeSelf);
        }

        // Clone
        if (Input.GetKeyDown(KeyCode.C) && mouseOver)
        {
            GameManager.log(gameObject.name + " was Cloned", mainPlayersCard);
            Vector2 v = new Vector2(transform.position.x + 50, transform.position.y + 50);
            cardManager.deck.cloneCard(cardManager.cardReference, v);
        }

        // Expose
        if (Input.GetKeyDown(KeyCode.E) && mouseOver)
        {
            exposed = !exposed;
            print(name + "'s exposed is set to: " + exposed);
        }

        // Go to top of deck
        if (Input.GetKeyDown(KeyCode.T) && mouseOver)
        {
            GameManager.log(gameObject.name + " added to the top of the deck", mainPlayersCard);
            cardManager.deck.topOfDeck(cardManager.cardReference, cardManager.isInMainDeck, exposed);
            Destroy(gameObject);
        }

        // Go to bottom of deck
        if (Input.GetKeyDown(KeyCode.B) && mouseOver)
        {
            GameManager.log(gameObject.name + " added to the bottom of the deck", mainPlayersCard);
            cardManager.deck.bottomOfDeck(cardManager.cardReference, cardManager.isInMainDeck, exposed);
            Destroy(gameObject);
        }

        // Go to bottom
        if (Input.GetKeyDown(KeyCode.DownArrow) && mouseOver)
        {
            transform.SetAsFirstSibling();
        }

        //Destroy (mainly for tokens)
        if (Input.GetKeyDown(KeyCode.X) && mouseOver)
        {
            GameManager.log(gameObject.name + " was removed from the game", mainPlayersCard);
            Destroy(gameObject);
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
        {
            GameManager.log($"{gameObject.name} was moved from {currentParent.name} to {(newParent.GetComponent<CardManager>() ? newParent.parent.name : newParent.name)}", mainPlayersCard);
            currentParent = newParent;
        }

        GameManager.Instance.itemBeingDragged = null;
        canvasGroup.blocksRaycasts = true;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.tag == "Counter")
                eventData.pointerDrag.transform.SetParent(transform);
            else// if (eventData.pointerDrag.tag == "Card")
            {
                eventData.pointerDrag.transform.SetParent(transform.parent);
                eventData.pointerDrag.GetComponent<CardScript>().newParent = transform;
                eventData.pointerDrag.transform.position = transform.parent.position;
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    eventData.pointerDrag.transform.SetAsFirstSibling();
                }
                if (Input.GetKey(KeyCode.Q))
                {

                    eventData.pointerDrag.GetComponent<CardScript>().faceDownCover.SetActive(!faceDownCover.activeSelf);
                    eventData.pointerDrag.transform.SetAsFirstSibling();
                    eventData.pointerDrag.GetComponent<CardScript>().ToggleActive();
                    GameManager.log(gameObject.name + " was placed facedown", mainPlayersCard);
                    GameManager.log(gameObject.name + (active ? " was Actived" : " was Exhausted"), mainPlayersCard);
                }
            }
        }
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
            GameManager.log(gameObject.name + (active ? " was Actived" : " was Exhausted"), mainPlayersCard);
        }
    }

    public void enhanceCard()
    {
        cardManager.cardReference.changeEnhancedCard();
        GameManager.Instance.magnifiedCard.SetActive(true);
        GameManager.Instance.magnifier.SetActive(true);
    }

    public void ToggleActive()
    {
        if (active)
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
        active = !active;
    }

}