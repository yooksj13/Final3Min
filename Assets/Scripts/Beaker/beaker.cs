using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beaker : MonoBehaviour
{
    
    [HideInInspector]
    public int food = 0;
    
    public int liquid { get; private set; }
    public int[] item { get; private set; } = new int[4];
    public int cooked { get; private set; }
    public int itemCnt { get; private set; }
    [SerializeField] private Sprite[] BeakerImage;

    int foodCnt;

    float waitTime;
    private Coroutine beakerTimer;

    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject smoke;
    private Animator animator;
    private ScaleUp smoky;

    void OnEnable() {
        food = 7;
        liquid = 0;
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = 0;
        }
        cooked = 0;
        itemCnt = 0;
        foodCnt = 0;
        waitTime = 7.0f;

        smoky = smoke.GetComponent<ScaleUp>();
        animator = this.GetComponent<Animator>();
        animator.SetInteger("color", 0);
        animator.SetInteger("cooked", cooked);
        this.GetComponent<SpriteRenderer>().sprite = BeakerImage[0];
    }

        void Update()
    {
        if (food !=7)
        {
            checkFood(food);
            food = 7;
        }
    }

    #region check food
    void checkFood(int foodType)
    {
        if (foodType < 4)
        {
            itemCnt++;
            item[foodType]++;
        }
        else if (foodType < 7)
        {
            liquid = foodType;
            changeColor(liquid);
        }
        foodCnt++;
        timerCheck(foodCnt);
        return;
    }
    #endregion

    #region change color
    void changeColor(int baseColor) {
        animator.SetInteger("color", baseColor);
    }
    #endregion

    #region beaker timer
    void timerCheck(int count)
    {
        if (count == 1)
        {
            beakerTimer = StartCoroutine(StartTimer());
            fire.SetActive(true);
        }
        else if (count == 2)
        {
            AddExtraTime();
        }
    }

    IEnumerator StartTimer()
    {
        while (waitTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            waitTime--;
        }
        StartCoroutine(TimerFinished());
    }

    IEnumerator TimerFinished()
    {
        int timeExm = 6;
        smoke.gameObject.SetActive(true);
        smoky.playSmoke();
        cooked = 1;
        animator.SetInteger("cooked", cooked);//1
        while (timeExm>0) {
            yield return new WaitForSeconds(1f);
            timeExm--;
        }
        cooked = 2;
        animator.SetInteger("cooked", cooked);
        this.GetComponent<beaker>().enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        StopCoroutine(beakerTimer);
        beakerTimer = StartCoroutine(StartTimer());
    }

    public void StopCouroutine() {        
        StopAllCoroutines();
    }
    #endregion

    public void initiate()
    {
        StopCouroutine();
        fire.SetActive(false);
        animator.SetInteger("color", 0);
        this.enabled = false;
    }
}
