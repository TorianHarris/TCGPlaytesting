using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class numModifer : MonoBehaviour, IPointerClickHandler
{
    public int startingNum;
    private string nameInLog;
    TMP_Text txt;
    int currentNum;
    Color origColor;
    public bool alterColors = true;

    // Start is called before the first frame update
    void Start()
    {
        nameInLog = transform.GetComponentInParent<CardManager>() ? $"{transform.GetComponentInParent<CardManager>().name}'s {gameObject.name}" : transform.parent.name;
        txt = GetComponent<TMP_Text>();
        int.TryParse(txt.text, out startingNum);
        //txt.text = startingNum.ToString();
        currentNum = startingNum;
        origColor = txt.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            decreaseNum();
        }

        else
        {
            increaseNum();
        }
    }
    public void decreaseNum()
    {
        currentNum--;
        GameManager.holdLog(nameInLog + " was changed: ", currentNum + 1, currentNum);
        displayNum();
    }

    public void increaseNum()
    {
        currentNum++;
        GameManager.holdLog(nameInLog + " was changed: ", currentNum - 1, currentNum);
        displayNum();
    }

    private void displayNum()
    {
        txt.text = currentNum.ToString();
        if (alterColors)
        {
            if (currentNum > startingNum)
                txt.color = new Color32(1, 139, 1, 255);
            else if (currentNum < startingNum)
                txt.color = Color.red;
            else
                txt.color = origColor;
        }
    }
}
