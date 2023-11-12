using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MinigameCircle 오브젝트 만들어서 부착, 오브젝트 관계: Pestle > MinigameBar > MinigameRect > MinigameCircle

public class MinigameCircle : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;

    private float BarPosX;
    private float BarScaleX;
    private Vector3 dir = Vector3.right;
    void Start(){
        BarPosX = transform.parent.parent.position.x;
        BarScaleX = transform.parent.parent.lossyScale.x;
    }
    void Update()
    {
        if(transform.position.x + transform.lossyScale.x * 2 > BarPosX + BarScaleX/2){
            if(dir==Vector3.right)
                dir = Vector3.left;
        }else if(transform.position.x - transform.lossyScale.x/3 < BarPosX - BarScaleX/2){
            if(dir==Vector3.left)
                dir = Vector3.right;
        }
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
