using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPotion : MonoBehaviour
{
    [HideInInspector]
    public int sBaseLiquid = 0;
    
    [HideInInspector]
    public int[] sItemArray = new int[4];
    
    [HideInInspector]
    public int sCooked = 0;
    
    [HideInInspector]
    public int sItemCnt = 0;

    public OrderManager comparePotion;
    [SerializeField]
    private Animator animator;
    
    public float fadeOutTime = 0.7f;

    private SpriteRenderer spriteRenderer;
    private bool flag = false;

    [SerializeField] private AudioClip fillSound;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
            if (flag) return;
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = Mathf.Clamp(mousepos.x, -8.5f, 8.5f);
            float y = Mathf.Clamp(mousepos.y, -4.5f, 4.5f);
            transform.position = new Vector2(x, y);
            
    }

    void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Beaker", "Submit"));
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Beaker")
            {
                if (sBaseLiquid != 0) return;
                gameObject.GetComponent<AudioSource>().PlayOneShot(fillSound);
                beaker beakerCtrl = hit.collider.gameObject.GetComponent<beaker>();
                beakerCtrl.gameObject.GetComponent<AudioSource>().Stop();
                beakerCtrl.initiate();
                sendToPotion(beakerCtrl);
            }
            else if (hit.collider.gameObject.tag == "Submit") {
                comparePotion.SubmitPotion(sBaseLiquid, sCooked, sItemArray);
                GameManager.instance.MouseHasObject = false;
                flag = true;
                StartCoroutine(FadeOut());
            }
        }
    }

    void sendToPotion(beaker beakerCtrl) {
        int color_cook = 0;
        sBaseLiquid = beakerCtrl.liquid;
        Array.Copy(beakerCtrl.item, sItemArray, 4);
        sCooked = beakerCtrl.cooked;
        sItemCnt = beakerCtrl.itemCnt;

        if (sBaseLiquid == 4 && sCooked == 0)
        {
            color_cook = 40;
        }
        else if (sBaseLiquid == 4 && sCooked == 1) {
            color_cook = 41;
        }
        else if (sBaseLiquid == 5 && sCooked == 0)
        {
            color_cook = 50;
        }
        else if (sBaseLiquid == 5 && sCooked == 1)
        {
            color_cook = 51;
        }
        else if (sBaseLiquid == 6 && sCooked == 0)
        {
            color_cook = 60;
        }
        else if (sBaseLiquid == 6 && sCooked == 1)
        {
            color_cook = 61;
        }
        else if (sCooked == 2)
        {
            color_cook = 70;
        }
        animator.SetInteger("color_cook", color_cook);
        beakerCtrl.enabled = true;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsedTime / fadeOutTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
