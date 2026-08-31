using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAi : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource source;
    public AudioClip attack, hurt;

    [Header("Public Assigned Variables")]
    public Animator Anim;
    public Collider hitBox;
    public Transform Player;
    public Slider HealthBar;
    public TMP_Text HealthTx;
    public GameObject WinScreen;
    public ParticleSystem BoomMazzule;
    public PlayerController PlayerController;

    [Header("Public Variables")]
    public float BossSpeed = 10;
    public float AttackRange = 2;
    public float MaxHealth = 2000;
    public static float BossCurrentHealth;

    [Header("Private Variables")]
    private NavMeshAgent Agent;

    // Start is called before the first frame update
    void Start()
    {
        BossCurrentHealth = MaxHealth;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = BossCurrentHealth;

        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();

        Agent.speed = BossSpeed;

        BossCurrentHealth = Mathf.Clamp(BossCurrentHealth, 0, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HealthTx.text = BossCurrentHealth + "%".ToString();

        float Distance = Vector3.Distance(transform.position, Player.position);

        if (Distance < AttackRange)
        {
            Attack();
        }
        else
        {
            Walk();
        }
    }

    public void BossTakeDamage(float Damage)
    {
        BossCurrentHealth -= Damage;

        if (source && hurt != null)
        {
            source.PlayOneShot(hurt);
        }

        HealthBar.value = BossCurrentHealth;

        if (BossCurrentHealth <= 0)
        {
            Instantiate(BoomMazzule, transform.position, Quaternion.identity);
            WinScreen.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }

    public void Attack()
    {
        Agent.isStopped = true;
        Anim.SetFloat("Speed", 0f);
        Anim.SetTrigger("Attack");

        Debug.Log("Attack");
    }

    void Walk()
    {
        Agent.isStopped = false;
        Anim.SetFloat("Speed", 0.2f);
        Agent.SetDestination(Player.position);
    }
}
