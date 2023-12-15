using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator amin;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake(){
        currentHealth = startingHealth;
        amin = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    

    public void TakeDamage(float _damaege){
        currentHealth = Mathf.Clamp(currentHealth - _damaege, 0, startingHealth);
        
        if(currentHealth > 0){
            amin.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        } else {
            if(!dead){
                amin.SetTrigger("die");
                //player
                if(GetComponent<PlayerMove>() != null)
                    GetComponent<PlayerMove>().enabled = false;

                //monter
                if(GetComponentInParent<MonterPatrol>() != null)
                    GetComponentInParent<MonterPatrol>().enabled = false;

                if(GetComponent<Monter>() != null)    
                    GetComponent<Monter>().enabled = false;

                dead = true;
            }
         
        }
    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    // private void Update(){
    //     if (Input.GetKeyDown(KeyCode.E))
    //         TakeDamage(1);
    // }

    private IEnumerator Invunerability(){
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for(int i = 0; i < numberOfflashes; i++){
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

        //Respawn
    public void Respawn(){
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
