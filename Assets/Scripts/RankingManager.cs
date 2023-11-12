using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    List<int> rankList;

    private readonly string RANK_KEY = "RANK";
    private readonly int RANK_MAX = 3;
    public GameObject stamp;

    // Start is called before the first frame update
    void Start()
    {
        int currentScore;
        if (PlayerPrefs.HasKey("CurrentScore"))
        {
            currentScore = PlayerPrefs.GetInt("CurrentScore");
        } else
        {
            currentScore = 0;
        }
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentScore.ToString();

        if (currentScore >= GameManager.instance.SUCCESS_STD)
        {
            AddNewScore(currentScore);
        }

        rankList = GetRankingList();
        int cnt = rankList.Count;

        for (int i=0; i < cnt; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text = rankList[i].ToString();
        }
        for (int i=cnt; i < 3; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text = "-";
        }

        int currentRank = rankList.IndexOf(currentScore);

        if (currentRank >= 0)
        {
            stamp.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> GetRankingList()
    {
        if (!PlayerPrefs.HasKey(RANK_KEY))
        {
            return new List<int>();
        }
        else
        {
            return new List<string>(PlayerPrefs.GetString(RANK_KEY).Split("\n")).ConvertAll(int.Parse);
        }
    }

    public void AddNewScore(int money)
    {
        List<int> rankList = GetRankingList();
        if (rankList.Count < RANK_MAX || rankList[rankList.Count - 1] < money)
        {
            rankList.Add(money);
            //sort descending
            rankList.Sort((a, b) => b.CompareTo(a));
            PlayerPrefs.SetString(RANK_KEY, string.Join("\n", rankList));
        }
    }
}
