using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour
{
    public float sensitivty = 0.5f;
    public Vector2 swipeDelta;

    private void Update()
    {

    }
    // Start is called before the first frame update
    public void OnSwipe(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if (pointerData != null)
        {
            swipeDelta = pointerData.delta * sensitivty;
        }
        

    }
}
