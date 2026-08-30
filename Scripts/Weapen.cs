using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class weapen_ : MonoBehaviour
{
    public enum FireType { SemiAuto, Auto };
    public FireType fireType = FireType.SemiAuto;

    public ParticleSystem MuzzleFlashes;
    public BossAi BossAi;
    public Animator Anim;
    public Camera PlayerCamera;
    public bool isButtonHolding = false;
    public static float GunDamage = 10;
    public float FireRange = 100f;
    public int Ammo = 50;

    [Header("Text")]
    public TMP_Text GunTx;
    public TMP_Text AmmoTx;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip ReloadSound, ShootSound;

    private float FireRate = 0.5f;
    private float NextTimeToFire = 0f;

    // Update is called once per frame
    private void Start()
    {
    }

    void Update()
    {
        Ammo = Mathf.Clamp(Ammo, 0, 50);

        GunTx.text = "GUN /D: " + GunDamage.ToString();
        AmmoTx.text = "AMMO: " + Ammo.ToString();

        if (fireType == FireType.SemiAuto)
        {
            if (isButtonHolding && Time.time >= NextTimeToFire)
            {
                Fire(10);
                NextTimeToFire = Time.time + FireRate;
            }
        }
        else if (fireType == FireType.Auto)
        {
            if (isButtonHolding && Time.time >= NextTimeToFire)
            {
                Fire(10);
                NextTimeToFire = Time.time + FireRate;
            }
        }
    }

    public void Reload()
    {
        Anim.SetTrigger("reload");

        if (source && ReloadSound != null)
        {
            source.PlayOneShot(ReloadSound);
        }
    }

    public void PointerSemiAutoFireDown()
    {
        isButtonHolding = true;
    }

    public void PointerDownFire()
    {
        isButtonHolding = true;
    }

    public void PointerUpFire()
    {
        isButtonHolding = false;
    }

    void Fire(float damage)
    {
        Ammo -= 1;

        if (Ammo <= 0)
        {
            return;
        }

        if (MuzzleFlashes != null)
        {
            MuzzleFlashes.Play();
            Anim.SetTrigger("Shoot");
        }

        if (source && ShootSound != null)
        {
            source.PlayOneShot(ShootSound);
        }

        RaycastHit hit;

        if (Physics.Raycast(
            PlayerCamera.transform.position,
            PlayerCamera.transform.forward,
            out hit,
            FireRange))
        {
            EnemyAi EnemyAi = hit.transform.GetComponent<EnemyAi>();
            Animator Anim = hit.transform.GetComponent<Animator>();

            Debug.Log("HIT : " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy"))
            {
                if (EnemyAi != null)
                {
                    EnemyAi.EnemyTakeDamage(GunDamage);
                }
            }

            if (hit.transform.CompareTag("Boss"))
            {
                if (BossAi != null)
                {
                    BossAi.BossTakeDamage(GunDamage);
                }
            }
        }
    }
}
