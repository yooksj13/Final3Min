using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    [HideInInspector]
    public bool Isfull = false;
    [HideInInspector]
    public bool IsCooked = false;
    [HideInInspector]
    public bool GetIn = false;
    [HideInInspector]
    public int Item; // 0,1,2,3

    [SerializeField]
    private GameObject[] CookedItem;

    [SerializeField]
    private Sprite[] BeforeCook;

    [SerializeField]
    private Sprite[] AfterCook;
    private Sprite DefaultSprite;

    private Animator anim;
    GameObject MortarDefault;
    GameObject MortarGo;

    private bool MinigameStage = false;
    GameObject MinigameBar;  // Pestle의 하위 오브젝트
    GameObject MinigameRect;  // MinigameBar의 하위 오브젝트
    GameObject MinigameCircle;  // MinigameRect의 하위 오브젝트

    void Start() {
        DefaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        anim = gameObject.GetComponent<Animator>();

        MortarDefault = transform.Find("MortarDefault").gameObject;
        MortarGo = transform.Find("MortarGo").gameObject;

        MinigameBar = transform.Find("MinigameBar").gameObject;
        MinigameRect = MinigameBar.transform.Find("MinigameRect").gameObject;
        MinigameCircle = MinigameRect.transform.Find("MinigameCircle").gameObject; 
    }

    void Update()
    {
        if(GetIn){
            Isfull = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = BeforeCook[Item];
            anim.SetInteger("item",Item);
            gameObject.layer = 0;
            GetIn = false;
        }
        
    }
    void OnMouseDown(){
        if(MinigameStage && !GameManager.instance.MouseHasObject){      
            double posX = MinigameCircle.transform.localPosition.x;
            
            anim.enabled = false;
            MortarDefault.SetActive(true);
            MortarGo.SetActive(false);
            gameObject.GetComponent<AudioSource>().Stop();

            if(posX < 0.5 && posX > -0.5)
            {
                IsCooked=true;
                gameObject.GetComponent<SpriteRenderer>().sprite = AfterCook[Item];
            }else{
                gameObject.GetComponent<SpriteRenderer>().sprite = BeforeCook[Item];
            }
            MinigameBar.SetActive(false);
            MinigameRect.SetActive(false);
            MinigameCircle.SetActive(false);
            MinigameStage = false;
        }
        else{     
            if(Isfull && !GameManager.instance.MouseHasObject){
                if(!IsCooked){  // 빻지 않은 상태라면
                    MortarDefault.SetActive(false);
                    MortarGo.SetActive(true);
                    anim.enabled = true;
                    gameObject.GetComponent<AudioSource>().Play();

                    MinigameStage = true; 

                    float end = (float)0.5*(1-MinigameRect.transform.localScale.x);
                    MinigameRect.transform.localPosition = new Vector3(Random.Range(0f,end-(float)0.1),0,0);  // 랜덤 영역 생성
                    
                    Transform t = MinigameBar.transform;
                    MinigameCircle.transform.position = new Vector3(t.position.x - t.lossyScale.x/2 + MinigameCircle.transform.lossyScale.x/3, t.position.y, t.position.z);

                    MinigameBar.SetActive(true);
                    MinigameRect.SetActive(true);
                    MinigameCircle.SetActive(true);
                }
                else{  // 빻은 상태라면
                    GameManager.instance.MouseHasObject = true;
                    gameObject.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
                    Instantiate(CookedItem[Item], transform.position, Quaternion.identity);

                    gameObject.layer = 8;
                    Isfull = false;
                    IsCooked = false;
                }
            }
            
        }
    }
}
