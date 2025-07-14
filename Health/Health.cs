using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float maxHealth => startingHealth;
    private Animator amin;
    private bool dead;
    private BoxCollider2D boxCollider;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Healthbar")]
    private GameObject healthbarInstance;
    private Coroutine hideHealthBarCoroutine;
    public float hideHealthBarDelay = 2f;
    public GameObject healthbarPrefab;

    [Header("Soud")]
    [SerializeField] private AudioClip hurtClip;

    public bool isPlayer = false;

    private void Awake()
    {
        currentHealth = startingHealth;
        amin = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        // isPlayer = true;
    }

    public void AssignHealthbar(GameObject hb)
    {
        healthbarInstance = hb;
        healthbarInstance.SetActive(false);
    }


    public void TakeDamage(float _damage)
    {
        // Debug.Log("TakeDamage called: " + _damage);

        if (invulnerable || dead) return;

        // Debug.Log($"Before damage: {currentHealth}/{startingHealth}, invulnerable: {invulnerable}");
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        // Debug.Log($"After damage: {currentHealth}/{startingHealth}");

        if (healthbarInstance != null)
        {
            Image healthbarFill = healthbarInstance.transform.Find("Fill")?.GetComponent<Image>();
            if (healthbarFill != null)
            {
                healthbarFill.fillAmount = currentHealth / startingHealth;
            }

            healthbarInstance.SetActive(true);

            if (hideHealthBarCoroutine != null)
                StopCoroutine(hideHealthBarCoroutine);

            hideHealthBarCoroutine = StartCoroutine(HideHealthBarAfterDelay(hideHealthBarDelay));
        }

        // Nếu chưa có healthbarInstance, tìm trong con của quái
        if (healthbarInstance == null)
        {
            Transform bar = transform.GetComponentInChildren<Canvas>(true)?.transform.Find("HealthbarMonter");
            if (bar != null)
            {
                healthbarInstance = bar.gameObject;
                healthbarInstance.SetActive(false); // Ẩn lúc đầu
            }
        }

        // Hiện Healthbar khi bị dame
        if (healthbarInstance != null)
        {
            healthbarInstance.SetActive(true);

            if (hideHealthBarCoroutine != null)
                StopCoroutine(hideHealthBarCoroutine);

            hideHealthBarCoroutine = StartCoroutine(HideHealthBarAfterDelay(hideHealthBarDelay));
        }

        // Xử lý animation hurt và die
        if (currentHealth > 0)
        {
            amin.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                amin.SetTrigger("die");

                if (GetComponent<PlayerMove>() != null)
                    GetComponent<PlayerMove>().enabled = false;

                if (GetComponentInParent<MonterPatrol>() != null)
                    GetComponentInParent<MonterPatrol>().enabled = false;

                if (GetComponent<Monter>() != null)
                    GetComponent<Monter>().enabled = false;

                Transform trigger = transform.Find("EnemyTrigger");
                if (trigger != null)
                    trigger.GetComponent<Collider2D>().enabled = false;

                if (isPlayer)
                {
                    UIManager uiManager = FindObjectOfType<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.GameOver();
                    }
                }

                Invoke("Deactivate", 1f);
                dead = true;

                if (!isPlayer)
                {
                    AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
                    if (audioManager != null && audioManager.enemyDeathClip != null)
                    {
                        audioManager.PlaySFX(audioManager.enemyDeathClip);
                        // Debug.Log("Die SFX enemyDeathClip.");
                    }
                }
            }
        }
    }


    private IEnumerator HideHealthBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (healthbarInstance != null)
            healthbarInstance.SetActive(false);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void AddHealthPercent(float percent)
    {
        float amountToHeal = startingHealth * percent;
        AddHealth(amountToHeal);
    }

    private IEnumerator Invunerability()
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int montersLayer = LayerMask.NameToLayer("Monters");

        Physics2D.IgnoreLayerCollision(playerLayer, montersLayer, true);

        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }

        invulnerable = true;
        yield return new WaitForSeconds(1f); // hoặc thời gian bất kỳ

        Physics2D.IgnoreLayerCollision(playerLayer, montersLayer, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;  // Tắt Collider nếu tồn tại

        if (amin != null)
            amin.enabled = false;

        gameObject.SetActive(false);
    }

    //Respawn
    public void Respawn()
    {
        AddHealth(startingHealth);
        amin.ResetTrigger("die");
        amin.Play("Idle");
        StartCoroutine(Invunerability());
        dead = false;

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}