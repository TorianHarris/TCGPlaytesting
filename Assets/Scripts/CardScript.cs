using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDropHandler
{
    private Transform canvas;
    private CardManager cardManager;
    [HideInInspector]
    public GameObject deck;
    [HideInInspector]
    public Transform currentParent;
    [HideInInspector]
    public Transform newParent;

    public bool active = true;
    public bool mouseOver = false;

    [HideInInspector]
    public GameObject faceDownCover;
    private GameObject iceOverlay;
    //public static GameObject itemBeingDragged;
    private CanvasGroup canvasGroup;
    private bool exposed;
    private bool resized;
    private Vector3 origSize;


    public void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Play Area").transform;
        cardManager = gameObject.GetComponent<CardManager>();
        faceDownCover = cardManager.faceDownCover;
        iceOverlay = cardManager.iceOverlay;
        canvasGroup = GetComponent<CanvasGroup>();
        currentParent = transform.parent;
        origSize = GetComponent<RectTransform>().localScale;
    }

    public void Update()
    {
        // Shuffle into deck
        if (Input.GetKeyDown(KeyCode.S) && mouseOver)
        {
            string s = string.Format("Shuffled {0} back into the deck", name);
            print(s);
            cardManager.deck.backToDeck(cardManager.cardReference, cardManager.isInMainDeck, exposed);
            Destroy(gameObject);
        }

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

        // Clone
        if (Input.GetKeyDown(KeyCode.C) && mouseOver)
        {
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
            string s = string.Format("Added {0} to the top of the deck", name);
            print(s);
            cardManager.deck.topOfDeck(cardManager.cardReference, cardManager.isInMainDeck, exposed);
            Destroy(gameObject);
        }

        // Go to bottom of deck
        if (Input.GetKeyDown(KeyCode.B) && mouseOver)
        {
            string s = string.Format("Added {0} to the bottom of the deck", name);
            print(s);
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
            string s = string.Format("Removed {0} from the game", name);
            print(s);
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
            currentParent = newParent;
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
        }
    }

    public void enhanceCard()
    {
        cardManager.cardReference.changeEnhancedCard();
        GameManager.Instance.enhancedCardPanel.SetActive(true);
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