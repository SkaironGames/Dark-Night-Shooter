using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamage : MonoBehaviour
{
    private PlayerController controller;
    private  Animator playerAnim;
    public int Damage = 50;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.TeckDemage(Damage);
            playerAnim.SetTrigger("sheck");
           
        }
    }
}

