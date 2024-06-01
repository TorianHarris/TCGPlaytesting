using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChildrenInParentCounter : MonoBehaviour
{
    public int threshold;
    public int modifier;

    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // display number if children exceeds the set number
        // -1 as to not count this gameobject itself

        if (transform.parent.childCount - 1 > threshold)
        {
            text.text = (transform.parent.childCount - 1 + modifier).ToString();
            text.enabled = true;
        }
        else
            text.enabled = false;
    }
}
