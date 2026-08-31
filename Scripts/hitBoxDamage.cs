using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamage : MonoBehaviour
{
    private PlayerController controller;
    public int Damage = 50;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.TeckDemage(Damage);
           
        }
    }
