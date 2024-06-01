using UnityEngine;
using UnityEngine.EventSystems;
//using LeanTween;

public class SlideOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 targetPosition; // The position the UI object will slide to when hovered over.
    public float hoverTimeThreshold = 1.0f; // The time (in seconds) to trigger the slide action.
    public float slideDuration = 0.5f; // The duration (in seconds) of the slide animation.

    private Vector2 initialPosition;
    private bool isHovering;
    private float hoverTimer;

    private void Start()
    {
        initialPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update()
    {
        if (isHovering)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= hoverTimeThreshold)
            {
                SlideToTargetPosition();
            }
        }
        else
        {
            hoverTimer = 0f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        //SlideToTargetPosition();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        SlideBackToInitialPosition();
    }

    private void SlideToTargetPosition()
    {
        isHovering = false;
        LeanTween.move(GetComponent<RectTransform>(), targetPosition, slideDuration).setEase(LeanTweenType.easeOutExpo);
    }

    private void SlideBackToInitialPosition()
    {
        LeanTween.move(GetComponent<RectTransform>(), initialPosition, slideDuration).setEase(LeanTweenType.easeOutExpo);
    }
}
