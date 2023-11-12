using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Liquid;

    [SerializeField]
    private int price; 

    void OnMouseDown(){
        if(GameManager.instance.MouseHasObject) return;
        gameObject.GetComponent<AudioSource>().Play();
        GameManager.instance.MouseHasObject = true;
        GameManager.instance.ChangeMoney(-price);
        Instantiate(Liquid,transform.position,Quaternion.identity);
    }
}
