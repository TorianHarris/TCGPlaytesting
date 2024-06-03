using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMGtoSO : MonoBehaviour
{
    //string imageFolderPath = "Assets/zz_Images_To_Construct";
    string scriptableObjectFolderPath = "Assets/zz_Constructed_Cards_To_Sort/";
    [InspectorButton("GenerateCards", ButtonWidth = 160)]
    public bool Generate;
    public List<Sprite> sprites = new List<Sprite>();

    public void GenerateCards()
    {


        foreach (Sprite sprite in sprites)
        {
            Card card = ScriptableObject.CreateInstance<Card>();
            AssetDatabase.CreateAsset(card,$"{scriptableObjectFolderPath}/{sprite.name}.asset");
            card.name = sprite.name;
            card.cardArt = sprite;
        }
    }
}
