using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLeft : MonoBehaviour
{
    public GameObject[] bottleList;
    public static int bottleLeft = 5;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject bottle in bottleList)
        {
            bottle.SetActive(true);
        }
        bottleLeft = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseBottleLeft()
    {
        if (bottleLeft < bottleList.Length)
        {
            for (int i = 0; i < bottleList.Length; i++)
            {
                if (!bottleList[i].activeSelf)
                {
                    bottleList[i].SetActive(true);
                    bottleLeft++;
                    WashMiniGame.bottleDirty--;
                    break;
                }
            }
        }
    }
}
