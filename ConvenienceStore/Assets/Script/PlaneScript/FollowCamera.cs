using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;


    void Start()
    {
        if(target  == null)
        {
            return;
        }    
        offsetX = transform.position.x - target.position.x;
        //가로로 움직일 것임.
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
