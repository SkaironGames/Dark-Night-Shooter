using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mouse_lock : MonoBehaviour
{
    
    float xRotation = 0f;
   
    // Update is called once per frame
    void Update()
    {
        xRotation -= Swipe.swipeDelta.y * 0.5f;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
