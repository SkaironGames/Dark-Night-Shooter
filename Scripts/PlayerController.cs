using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Public Assign Veriables")]
    public TMP_Text HealthTx;
    public TMP_Text ZombieKillTx;
    public GameObject GameOverScreen;
    public GameObject offPluseScreen;
    public GameObject offGunScreen;
    public Slider HealthBar;
    public Joystick Joystick;
    public weapen_ Weapen;

    [Header("Sounds")]
    public AudioSource Source;
    public AudioSource AudioSource;
    public AudioClip JumpSound, RunSound, DieSound, HitSound, WalkSound;

    [Header("Speeds")]
    public float WalkSpeed;
    public float RunSpeed;
    public float CurrentSpeed = 5;

    [Header("Healths")]
    public float MaxHealth = 100f;
    public float CurrentHealth;

    [Header("Grevity,JumpHight")]
    public float Grevity = -9.81f;
    public float JumpHight = 3f;

    [Header("Private")]
    private Animator Anim;
    private Vector3 Velocity;
    private CharacterController Controller;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;
        CurrentSpeed = WalkSpeed;

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
        Controller = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
        StartCoroutine(Halth());
    }

    // Update is called once per frame
    void Update()
    {
        HealthTx.text = CurrentHealth + "%".ToString();

        float Horizontal = Joystick.Horizontal;
        float Vertical = Joystick.Vertical;

        transform.Rotate(0, Swipe.swipeDelta.x * 0.5f, 0);
        Swipe.swipeDelta = Vector2.zero;

        if (Controller.isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        Velocity.y += Grevity * Time.deltaTime;

        Vector3 move = transform.right * Horizontal + transform.forward * Vertical;

        Controller.Move(move * CurrentSpeed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);

        float AnimSpeed = move.magnitude * CurrentSpeed;
        Anim.SetFloat("Speed", AnimSpeed);

        if (Mathf.Abs(Joystick.Horizontal) > 0.1f || Mathf.Abs(Joystick.Vertical) > 0.1f)
        {
            if (!Source.isPlaying)
            {
                Source.clip = WalkSound;
                Source.loop = true;
                Source.Play();
            }
        }
        else
        {
            Source.Stop();
        }
    }

    public void jumpDown()
    {
        if (Controller.isGrounded)
        {
            Velocity.y = Mathf.Sqrt(JumpHight * -2f * Grevity);

            if (Source && JumpSound != null)
            {
                AudioSource.PlayOneShot(JumpSound);
            }
        }
    }

    public void RunDown()
    {
        CurrentSpeed = 20;

        if (Source && RunSound != null)
        {
            Source.PlayOneShot(RunSound);
        }

        Debug.Log("RunSound press");
    }

    public void RunUp()
    {
        CurrentSpeed = 10;
    }

    public void Reload()
    {
        Weapen.Ammo = 50;
    }

    public void TeckDemage(float Demage)
    {
        CurrentHealth -= Demage;

        Source.PlayOneShot(HitSound, 2f);
        HealthBar.value = CurrentHealth;

        Debug.Log(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Source.PlayOneShot(DieSound);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Invoke("SourceOff", 0.5f);

            GameOverScreen.SetActive(true);
            ZombieKillTx.text = PlayerPrefs.GetInt("ZombiePluseCounter").ToString();

            Destroy(gameObject, 1f);
        }
    }

    void SourceOff()
    {
        Source.enabled = false;
    }

    IEnumerator Halth()
    {
        while (true)
        {
            if (CurrentHealth < 100)
            {
                yield return new WaitForSeconds(5);

                CurrentHealth += 1;
                HealthBar.value = CurrentHealth;
            }
            else
            {
                yield return null;
            }
        }
    }
}
