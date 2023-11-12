using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Stopwatch stopwatch;

    public int money = 0;
    
    public bool MouseHasObject = false;

    private readonly int TIME_LIMIT = 3 * 60 * 1000;
    public int SUCCESS_STD = 10;

    void Awake() {
        instance = this;
    }

        // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.stopwatch.ElapsedMilliseconds >= TIME_LIMIT)
        {
            PlayerPrefs.SetInt("CurrentScore", this.money);
            if (this.money >= SUCCESS_STD)
            {
                SceneManager.LoadScene("GameClear");
            } else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Play");
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void ChangeMoney(int x)
    {
        this.money += x;
    }

    public int GetMoney()
    {
        return this.money;
    }
}
