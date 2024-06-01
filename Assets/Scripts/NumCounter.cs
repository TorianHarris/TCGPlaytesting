using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumCounter : MonoBehaviour
{
    public Text text;
    public int num = 1;
    public int maxNum = 0;
    public bool resetNumOnMaxIncrease = true;
    private bool hasMaxNum;


    // Use this for initialization
    void Start()
    {
        if (maxNum > 0) hasMaxNum = true;
        updateTxt();
    }

    public void plusNum()
    {
        num++;
        updateTxt();
    }

    public void minusNum()
    {
        num--;
        updateTxt();
    }


    public void plusMaxNum()
    {
        maxNum++;
        if (resetNumOnMaxIncrease) num = maxNum;
        updateTxt();
    }

    public void minusMaxNum()
    {
        maxNum--;
        if (num > maxNum)
            num = maxNum;
        updateTxt();
    }

    public void updateTxt()
    {
        text.text = hasMaxNum ? num.ToString() + " / " + maxNum.ToString() : num.ToString();
    }
}
