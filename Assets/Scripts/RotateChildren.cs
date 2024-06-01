using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChildren : MonoBehaviour
{
    public bool rotate;
    public bool facedown;
    private int children;

    private void Start()
    {
        children = transform.childCount;    
    }

    private void OnTransformChildrenChanged()
    {
        if(children < transform.childCount)
        {
            if (rotate && transform.childCount > children)
            {
                GameObject card = transform.GetChild(transform.childCount - 1).gameObject;
                if (card.GetComponent<CardScript>().active)
                    card.GetComponent<CardScript>().ToggleActive();
            }

            if (facedown)
            {
                GameObject card = transform.GetChild(transform.childCount - 1).gameObject;
                if (card.GetComponent<CardManager>().faceDownCover)
                    card.GetComponent<CardManager>().faceDownCover.SetActive(true);
            }

            if (!rotate && !facedown)
            {
                GameObject card = transform.GetChild(transform.childCount - 1).gameObject;
                if (!card.GetComponent<CardScript>().active)
                    card.GetComponent<CardScript>().ToggleActive();
                if(card.GetComponent<CardManager>().faceDownCover)
                    card.GetComponent<CardManager>().faceDownCover.SetActive(false);
            }
        }
        children = transform.childCount;
    }
}
