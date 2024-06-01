using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CounterSpawner : MonoBehaviour, IPointerDownHandler
{

    public GameObject counterPrefab;
    private Transform canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Play Area").transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject counter = Instantiate(counterPrefab, canvas);
            counter.GetComponent<Draggable>().isDraggable = true;
            print("instantiate trueing");
        }
    }

}
