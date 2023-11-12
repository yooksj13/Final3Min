using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int ItemType;   // 0,1,2,3
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mousepos.x,-8.5f,8.5f);
        float y = Mathf.Clamp(mousepos.y,-4.5f,4.5f);
        transform.position = new Vector2(x,y);
    }

    void OnMouseDown(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Pestle","Bin"));
        if(hit){
            if(hit.collider.gameObject.tag == "Bin"){
                GameManager.instance.MouseHasObject = false;
                hit.collider.gameObject.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
            else{
                Pestle pestle = hit.collider.gameObject.GetComponent<Pestle>();
                if(!pestle.Isfull){
                    gameObject.GetComponent<AudioSource>().Play();
                    pestle.GetIn = true;
                    pestle.Item = ItemType;
                    GameManager.instance.MouseHasObject = false;

                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Destroy(gameObject, 1f);
                }
            }
        } 
    }

}
