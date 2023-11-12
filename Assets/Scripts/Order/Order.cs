using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    Stopwatch stopwatch;
    [SerializeField] private Slider _slider;
    [SerializeField] private Sprite[] CompleteImg;
    [SerializeField] private Sprite[] ItemImg;

    private readonly float ORDER_TIME = 42f * 1000;
    public bool timeOver;
    public int price;

    private readonly int BASE_COUNT = 3;
    private readonly int ITEM_COUNT = 4;

    private readonly int ITEM_IDX = 0;
    private readonly int COMPLETE_IDX = 2;

    private readonly int[] PRICE_PER_BASE = {7, 11, 9};
    private readonly int PRICE_PER_ITEM_COUNT = 5;


    private Color GREEN = Color.green;
    private Color YELLOW = Color.yellow;
    private Color RED = Color.red;

    private int baseNum;
    private List<int> itemList;


    private void Awake()
    {
        _slider.transform.GetChild(0).GetComponent<Image>().color = GREEN;

        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }
        SetRecipe();
    }


    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        timeOver = false;
        SetSliderValue(0);
    }


    void SetRecipe()
    {
        System.Random prng = new System.Random(Random.Range(0, int.MaxValue));

        // -- set base
        baseNum = prng.Next(0, BASE_COUNT);
        transform.GetChild(COMPLETE_IDX).GetComponent<SpriteRenderer>().sprite = CompleteImg[baseNum];

        // -- set items
        itemList = new List<int>();

        // number of item
        int itemCount = prng.Next(1, 3);

        // assign item
        for (int i = 0; i < itemCount; i++)
        {
            itemList.Add(1);
        }
        for (int i = 0; i < ITEM_COUNT - itemCount; i++)
        {
            itemList.Add(0);
        }

        // shuffle
        for (int i=0; i < ITEM_COUNT; i++)
        {
            int random_idx = prng.Next(i, ITEM_COUNT);

            int temp = itemList[random_idx];
            itemList[random_idx] = itemList[i];
            itemList[i] = temp;
        }

        int idx = ITEM_IDX;

        for (int i=0; i < ITEM_COUNT; i++)
        {
            if (itemList[i] > 0)
            {
                transform.GetChild(idx++).GetComponent<SpriteRenderer>().sprite = ItemImg[i];
            }
        }

        this.price = PRICE_PER_ITEM_COUNT * itemCount + PRICE_PER_BASE[baseNum];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!timeOver)
        {
            CheckTimer();
        }
        SetSliderValue(stopwatch.ElapsedMilliseconds / ORDER_TIME);
    }

    void CheckTimer()
    {
        if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3)
        {
            SetSliderColor(GREEN);
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3 * 2)
        {
            SetSliderColor(YELLOW);
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME)
        {
            SetSliderColor(RED);
        }
        else
        {
            StopTimer();
            timeOver = true;
        }
    }

    private void StopTimer()
    {
        stopwatch.Stop();
    }

    private void SetSliderValue(float value)
    {
        _slider.value = value;
    }

    private void SetSliderColor(Color c)
    {
        _slider.transform.GetChild(0).GetComponent<Image>().color = c;
    }

    public bool IsEqual(int baseNum, int[] items)
    {
        if (baseNum != this.baseNum + 4)
        {
            return false;
        }

        for (int i=0; i<ITEM_COUNT; i++)
        {
            if (items[i] != this.itemList[i])
            {
                return false;
            }
        }
        return true;
    }
}
