using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


public class CSVtoSO : MonoBehaviour
{
    public TextAsset CSV;
    public enum gameName { Aether, Blitz, Break, BXD, Dawn, Saga, Scions, Wistia, WitchAcademy  }
    public enum className { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }
    public gameName gameFolder;
    public className classFolder;
    public string altFolder;

    [InspectorButton("GenerateCards", ButtonWidth = 160)]
    public bool Generate;
    public void GenerateCards()
    {

        if (!CSV)
            throw new ArgumentException("You must provide a CSV.");

        Card.classColor clr = Card.classColor.Gray;

        switch (classFolder)
        {
            case className.Red:
                clr = Card.classColor.Red;
                break;
            case className.Yellow:
                clr = Card.classColor.Yellow;
                break;
            case className.Purple:
                clr = Card.classColor.Purple;
                break;
            case className.Green:
                clr = Card.classColor.Green;
                break;
            case className.Blue:
                clr = Card.classColor.Blue;
                break;
            case className.White:
                clr = Card.classColor.White;
                break;
            case className.Gray:
                clr = Card.classColor.Gray;
                break;
            case className.Orange:
                clr = Card.classColor.Orange;
                break;
            case className.Black:
                clr = Card.classColor.Black;
                break;
            default:
                break;
        }

        string folderToPopulate = gameFolder.ToString() + "/" + (altFolder != "" ? altFolder : classFolder.ToString());
        string[] allLines = CSV.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        allLines = allLines.Skip(1).ToArray();

        foreach (string s in allLines)
        {
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string[] splitData = CSVParser.Split(s);
            Card card;
            string guid;
            string[] guids = AssetDatabase.FindAssets(splitData[0].Replace("\"", ""), new[] { $"Assets/Resources/Cards/{folderToPopulate}/" });
            print(guids.Length);
            if (guids.Length == 0)
            {
                card = ScriptableObject.CreateInstance<Card>();
                AssetDatabase.CreateAsset(card, $"Assets/Resources/Cards/{folderToPopulate}/" + splitData[0].Replace("\"", "") + ".asset");
                guid = AssetDatabase.FindAssets(splitData[0].Replace("\"", ""), new[] { $"Assets/Resources/Cards/{folderToPopulate}/" })[0];
            }
            else
            {
                guid = guids[0];
            }

            print((Card)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Card)));
            card = (Card)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Card));
            card.name = splitData[0].Replace("\"", "");
            card.type = (Card.cardType)Enum.Parse(typeof(Card.cardType), splitData[1]);
            card.cost = splitData[2];
            card.power = splitData[3] == "" ? 0 : int.Parse(splitData[3]);
            card.health = splitData[4] == "" ? 0 : int.Parse(splitData[4]);
            card.trait = splitData[5].Replace("\"", "");
            card.mainEffect = splitData[6].Replace("\"", "");
            card.secondaryEffect = splitData[7].Replace("\"", "");
            card.color = clr;
        }
        AssetDatabase.SaveAssets();
    }
}
