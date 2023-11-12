using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WashMiniGame : MonoBehaviour
{
    public Slider progressBar;
    private float fillAmount = 0.0f;

    public Sprite washerFilled;
    public Sprite washerEmpty;
    private bool washMode = false;
    private SpriteRenderer spriteRenderer;

    public AudioSource washBGM;
    public AudioSource washClipBGM;
    public AudioClip[] washClips;
    private int spaceCnt;

    public static int bottleDirty = 0;
    private bool barActive;

    public BottleLeft bottleLeftScript;

    private void Start()
    {
        bottleDirty = 0;
        progressBar.value = fillAmount;
        progressBar.gameObject.SetActive(false);
        barActive = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (bottleDirty > 0 && barActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fillAmount += 0.1f;
                spaceCnt++;
                if (spaceCnt % 2 == 1)
                {
                    washClipBGM.PlayOneShot(washClips[0]);

                }
                else
                {
                    washClipBGM.PlayOneShot(washClips[1]);
                }
                UpdateProgressBar();
            }

            fillAmount -= Time.deltaTime * 0.2f;
            fillAmount = Mathf.Clamp01(fillAmount);
            UpdateProgressBar();

            if (fillAmount >= 1f)
            {
                bottleLeftScript.IncreaseBottleLeft();
                washBGM.Play();
                fillAmount = 0f;
                spaceCnt = 0;
                UpdateProgressBar();
                barActive = false;
                progressBar.gameObject.SetActive(false);
            }
        }
        if (bottleDirty > 0 && !washMode)
        {
            spriteRenderer.sprite = washerFilled;
        }
        else if (bottleDirty == 0)
        {
            spriteRenderer.sprite = washerEmpty;
            washMode = false;
        }

    }

    void UpdateProgressBar()
    {
        progressBar.value = fillAmount;
    }

    void OnMouseDown()
    {
        if (bottleDirty > 0)
        {
            progressBar.gameObject.SetActive(true);
            barActive = true;
        }
    }

    public void IncreaseDirtyBottle()
    {
        StartCoroutine(BottleUp());
    }

    IEnumerator BottleUp()
    {
        yield return new WaitForSeconds(3f);

        bottleDirty++;
    }

}
