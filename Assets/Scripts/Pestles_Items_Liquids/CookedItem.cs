using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedItem : MonoBehaviour
{
    [SerializeField]
    private int ItemType;   // 0,1,2,3
    private AudioSource binAudio;

    private void Start()
    {
        GameObject binObject = GameObject.Find("Bin");
        binAudio = binObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mousepos.x,-8.5f,8.5f);
        float y = Mathf.Clamp(mousepos.y,-4.5f,4.5f);
        transform.position = new Vector2(x,y);
    }

    void OnMouseDown(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Beaker","Bin"));
        if(hit){
            if(hit.collider.gameObject.tag == "Bin"){
                GameManager.instance.MouseHasObject = false;
                binAudio.Play();
                Destroy(gameObject);
            }
            else{
                beaker babyBeaker = hit.collider.gameObject.GetComponent<beaker>();
                if(babyBeaker.liquid != 0){
                    if(babyBeaker.itemCnt < 2 && babyBeaker.cooked == 0){
                        babyBeaker.food = ItemType;

                        GameManager.instance.MouseHasObject = false;
                        Destroy(gameObject);
                    }
                }
            }
        } 
    }
}
