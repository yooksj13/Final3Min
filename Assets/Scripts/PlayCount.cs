using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public SpriteRenderer[] countImages;
    public AudioSource playBGM;
    public AudioSource originBGM;
    public AudioClip[] countSounds;

    public GameObject blockObj;
    public OrderManager orderManager;

    private void Start()
    {
        foreach (SpriteRenderer sprite in countImages)
        {
            sprite.enabled = false;
        }

        StartCoroutine(DisplayImages());
    }

    IEnumerator DisplayImages()
    {
        for (int i = 0; i < countImages.Length; i++)
        {
            SpriteRenderer sprite = countImages[i];

            yield return new WaitForSeconds(0.5f);

            sprite.enabled = true;

            originBGM.PlayOneShot(countSounds[i]);

            yield return new WaitForSeconds(0.5f);

            sprite.enabled = false;
        }
        GameManager.instance.stopwatch.Start();
        blockObj.SetActive(false);
        orderManager.CreateFirstOrder();
        playBGM.Play();
    }
}
