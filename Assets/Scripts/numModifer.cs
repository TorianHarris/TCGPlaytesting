using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class numModifer : MonoBehaviour, IPointerClickHandler
{
    public int startingNum;
    public int increment = 1;
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
        currentNum = startingNum;
        origColor = txt.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //this is a comment
            decreaseNum();
        }

        else
        {
            increaseNum();
        }
    }
    public void decreaseNum()
    {
        currentNum-= increment;
        GameManager.holdLog(nameInLog + " was changed: ", currentNum + increment, currentNum);
        displayNum();
    }

    public void increaseNum()
    {
        currentNum+= increment;
        GameManager.holdLog(nameInLog + " was changed: ", currentNum - increment, currentNum);
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
