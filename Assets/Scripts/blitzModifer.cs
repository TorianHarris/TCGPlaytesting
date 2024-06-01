using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class blitzModifer : MonoBehaviour, IPointerClickHandler
{
    TMP_Text txt;
    public int num = 1;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            num--;
        else
            num++;
        txt.text = num.ToString();
    }

    public void goSecond()
    {
        num++;
        txt.text = num.ToString();
    }
}
