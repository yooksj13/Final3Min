using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleClick : MonoBehaviour
{
    public GameObject bottlePrefab;
    public GameObject orderBoard;
    private void OnMouseDown()
    {
        if(GameManager.instance.MouseHasObject) return; 
        GameManager.instance.MouseHasObject = true;
        GameObject bottleCreated = Instantiate(bottlePrefab);
        bottleCreated.GetComponent<fillPotion>().comparePotion = orderBoard.GetComponent<OrderManager>();
        gameObject.SetActive(false);
        BottleLeft.bottleLeft--;
    }
}
