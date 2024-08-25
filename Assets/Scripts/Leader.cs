using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLeader", menuName = "ScriptableObjects/NewLeader", order = 1)]
public class Leader : ScriptableObject
{
    public enum primaryClassColor { Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }
    public enum secondaryClassColor { None, Red, Yellow, Purple, Green, Blue, White, Gray, Orange, Black }

    [System.Serializable]
    public class leaderLevel
    {
        public string cardName;
        public int level;
        public int primaryValue;
        public int power;
        public int health;
        public primaryClassColor primaryColor;
        public secondaryClassColor secondaryColor;
        [TextArea]
        public string mainEffect;
        public string trait;
        public Sprite cardArt;

        public Color32 getColor(string color)
        {
            switch (color)
            {
                case "Red":
                    return GameManager.Instance.Red;
                case "Yellow":
                    return GameManager.Instance.Yellow;
                case "Purple":
                    return GameManager.Instance.Purple;
                case "Green":
                    return GameManager.Instance.Green;
                case "Blue":
                    return GameManager.Instance.Blue;
                case "White":
                    return GameManager.Instance.White;
                case "Gray":
                    return GameManager.Instance.Gray;
                case "Orange":
                    return GameManager.Instance.Orange;
                case "Black":
                    return GameManager.Instance.Black;
                default:
                    return Color.black;
            }
        }

        public void createLeader(GameObject c, Transform t)
        {
            GameObject leader = Instantiate(c, t);
            leader.GetComponent<LeaderManager>().FillLeaderInfo(this);
            leader.name = cardName;
            //return card;
        }
    }

    public leaderLevel[] LeaderLevels;


}
