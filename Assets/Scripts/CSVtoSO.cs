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
    public enum gameName { Aether, Anima, Blitz, Break, BXD, Dawn, Fortuna, LinkRush, Saga, Scions, Wistia, WitchAcademy }
    public enum className { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }
    public gameName gameFolder;
    public className classFolder;
    public string altFolder;


    public static List<string> ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                // handle escaped quote ""
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current += '"';
                    i++; // skip second quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current);
        return result;
    }
    //private static readonly Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
    [InspectorButton("GenerateCards", ButtonWidth = 160)]
    public bool Generate;
    public void GenerateCards()
    {

        if (!CSV)
            throw new ArgumentException("You must provide a CSV.");

        Card.classColor clr = Enum.TryParse(classFolder.ToString(), out Card.classColor c) ? c : Card.classColor.Gray;

        string folderToPopulate = gameFolder.ToString() + "/" + (altFolder != "" ? altFolder : classFolder.ToString());
        string[] allLines = CSV.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        allLines = allLines.Skip(1).ToArray();


        foreach (string s in allLines)
        {
            ;
            List<string> splitData = ParseCSVLine(s);

            //string guid;
            string cardName = splitData[0].Replace("\"", "");
            string path = $"Assets/Resources/Cards/{folderToPopulate}/{cardName}.asset";
            Card card = AssetDatabase.LoadAssetAtPath<Card>(path);

            if (card == null)
            {
                card = ScriptableObject.CreateInstance<Card>();
                AssetDatabase.CreateAsset(card, path);
            }
            // string[] guids = AssetDatabase.FindAssets(splitData[0].Replace("\"", ""), new[] { $"Assets/Resources/Cards/{folderToPopulate}/" });
            // print(guids.Length);
            // if (guids.Length == 0)
            // {
            //     card = ScriptableObject.CreateInstance<Card>();
            //     AssetDatabase.CreateAsset(card, $"Assets/Resources/Cards/{folderToPopulate}/" + splitData[0].Replace("\"", "") + ".asset");
            //     guid = AssetDatabase.FindAssets(splitData[0].Replace("\"", ""), new[] { $"Assets/Resources/Cards/{folderToPopulate}/" })[0];
            // }
            // else
            // {
            //     guid = guids[0];
            // }

            //print((Card)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Card)));
            //card = (Card)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Card));
            card.name = cardName;
            card.type = (Card.cardType)Enum.Parse(typeof(Card.cardType), splitData[1]);
            card.cost = splitData[2];
            card.power = splitData[3] == "" ? 0 : int.Parse(splitData[3]);
            card.health = splitData[4] == "" ? 0 : int.Parse(splitData[4]);
            card.trait = splitData[5];
            card.mainEffect = splitData[6];
            card.secondaryEffect = splitData[7];
            card.color = clr;
        }
        AssetDatabase.SaveAssets();
    }

}

