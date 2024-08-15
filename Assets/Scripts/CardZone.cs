using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.tag != "Counter")
        {
            GameObject item = GameManager.Instance.itemBeingDragged;
            item.transform.SetParent(transform);
            if (item.GetComponent<CardScript>())
                item.GetComponent<CardScript>().newParent = transform;
            else
                item.GetComponent<LeaderManager>().newParent = transform;
            eventData.pointerDrag.transform.position = transform.position;
        }
    }

    public void ActiveAllCards()
    {
        foreach (CardScript card in transform.GetComponentsInChildren<CardScript>())
        {
            if (card.iceOverlay.activeSelf) card.iceOverlay.SetActive(false);
            else if (!card.active) card.ToggleActive();
        }
    }

    public void ActiveAllLeaders()
    {
        foreach (LeaderManager leader in transform.GetComponentsInChildren<LeaderManager>())
        {
            if (!leader.active) leader.ToggleActive();
        }
    }

    public void IncreaseLeaderNum()
    {
        foreach (LeaderManager leader in transform.GetComponentsInChildren<LeaderManager>())
        {
            if (leader.GetComponentInChildren<numModifer>()) leader.GetComponentInChildren<numModifer>().increaseNum();
        }
    }
}
