using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;

    [SerializeField]
    private int price;

    void OnMouseDown(){
          if(GameManager.instance.MouseHasObject) return;
          gameObject.GetComponent<AudioSource>().Play();
          GameManager.instance.MouseHasObject = true;
          GameManager.instance.ChangeMoney(-price);
          Instantiate(Item,transform.position,Quaternion.identity);
    }
}
