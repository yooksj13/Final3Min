using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI moneyCnt;
    public TextMeshProUGUI timeCnt;

    // Start is called before the first frame update
    void Start()
    {
        moneyCnt.text = "666";
        timeCnt.text = "00 : 00";
    }

    // Update is called once per frame
    void Update()
    {
        moneyCnt.text = GameManager.instance.GetMoney().ToString();


        int sec = 180-(int)GameManager.instance.stopwatch.ElapsedMilliseconds / 1000;
        int min = sec / 60;

        timeCnt.text = string.Format("{0, 2:0} : {1, 2:00}", min, sec - min * 60);
    }
}
