using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    [SerializeField]
    private int liquidType;   // 4,5,6

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mousepos.x,-8.5f,8.5f);
        float y = Mathf.Clamp(mousepos.y,-4.5f,4.5f);
        transform.position = new Vector2(x,y);
    }

    void OnMouseDown(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 30, LayerMask.GetMask("Beaker","Bin")); 
        if(hit){
            if(hit.collider.gameObject.tag == "Bin"){
                GameManager.instance.MouseHasObject = false;
                hit.collider.gameObject.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
            else{
                beaker babyBeaker = hit.collider.gameObject.GetComponent<beaker>();
                if(babyBeaker.liquid == 0){
                    babyBeaker.food = liquidType;
                    GameManager.instance.MouseHasObject = false;
                    gameObject.GetComponent<AudioSource>().Play();
                    hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 1f);
                }
            }
        } 
    }

}
