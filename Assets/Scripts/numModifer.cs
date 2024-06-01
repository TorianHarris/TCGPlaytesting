using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class numModifer : MonoBehaviour, IPointerClickHandler
{
    CardManager card;
    TMP_Text txt;
    int currentNum;
    int originalNum;
    Color origColor;
    public bool alterColors = true;

    // Start is called before the first frame update
    void Start()
    {
        //card = transform.GetComponentInParent<CardManager>();
        txt = GetComponent<TMP_Text>();
        int.TryParse(txt.text, out originalNum);
        currentNum = originalNum;
        origColor = txt.color; 
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        //if(card.cardType.text == "Unit" || card.cardType.text == "Token Unit")
        //{
            if (eventData.button == PointerEventData.InputButton.Right)
                currentNum--;
            else
                currentNum++;
            txt.text = currentNum.ToString();
        if (alterColors)
        {
            if (currentNum > originalNum)
                txt.color = new Color32(1, 139, 1, 255);
            else if (currentNum < originalNum)
                txt.color = Color.red;
            else
                txt.color = origColor;
        }

        //}
    }
}
