using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieSpowner : MonoBehaviour
{
    public GameObject[] zombie;
    public Transform[] spwonPoint;
    public float zombieCounter = 5;
    private void Update()
    {
        if (zombieCounter <= 0&&BossComing.ZombiePulseCounter<50)
        {
            for (int i = 0; i < 5; i++)
            {
                Transform RandomPoint = spwonPoint[Random.Range(0, spwonPoint.Length)];
                Instantiate(zombie[Random.Range(0, zombie.Length)], RandomPoint.position, RandomPoint.rotation);

            }
            zombieCounter = 5;

        }

       
    }
}
