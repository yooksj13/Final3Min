using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAni : MonoBehaviour
{
    public int color = 0;
    public int progress = 0;

    private Animator animator;

    void Update()
    {
        if (animator.enabled)
        {
            animator.SetInteger("progress", progress);
            animator.SetInteger("color", color);
        }
    }

}
