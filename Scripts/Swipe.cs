using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour
{
    public float sensitivty = 0.5f;
    public static Vector2 swipeDelta;
    
    private void Update()
    {
        
    }
    // Start is called before the first frame update
    public void OnSwipe(PointerEventData eventData)
    {
        swipeDelta = eventData.delta * sensitivty;

    }
}
