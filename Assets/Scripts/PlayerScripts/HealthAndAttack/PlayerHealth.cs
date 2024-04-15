using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerHealth : MonoBehaviourPun
{
    [Header ("Health")]
    
    [SerializeField] public float playerHealth = 3f; // initial player health
    public float health { get; private set; } // Current player health
    private Animator anim; // Reference to the animator component
    private Rigidbody2D body; // Reference to the rigidbody component
    private bool dead; // bool determining death status
    PhotonView photonView; // Reference to the photonview component
    private SpawnPlayer spawnPlayer;
    public GameObject DieUi; // Reference to the DieUI game object
    public GameObject Rewards; // Reference to the rewards game object
    public GameObject Enemy; // Reference to the enemy game object

    [Header("IFrames")]
    [SerializeField] private float IFrameDuration; // duration of IFrames
    [SerializeField] private int numberOfFlashes; // number of flashes that IFrames does
    [SerializeField] private bool isBoss; // bool determinging whether boss
    private SpriteRenderer spriteRenderer; // Reference to the spriterenderer component

    // Start is called before the first frame update
    void Start()
    { // retreiving required components
        health = playerHealth;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        photonView = GetComponent<PhotonView>();
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, playerHealth);

        if (health > 0)
        {
            anim.SetTrigger("Hurt");
            photonView.RPC("PlayHurtAnimation", RpcTarget.All);
            StartCoroutine(Invunrability());
    
        }
        else
        {
            if (!dead)
            {
                photonView.RPC("PlayDeadAnimation", RpcTarget.All);
                PlayerMovement2D playerMovement = GetComponent<PlayerMovement2D>(); // get component
                    if (playerMovement != null) // disable player movement
                    {
                        playerMovement.enabled = false;
                    }
                    PlayerAttack playerAttack = GetComponent<PlayerAttack>();// get component
                if (playerAttack != null)
                    {
                        playerAttack.enabled = false; // disable player attack
                }

                    body.velocity = Vector2.zero; // Stop player movement
                    body.freezeRotation = true; // Freeze player rotation
                    dead = true; // Mark player as dead
                    Invoke("Respawn", 10f); // Respawn after a delay
            }
            //if (playerHealth == 0)
            //{

            //    PlayerMovement2D playerMovement = GetComponent<PlayerMovement2D>();
            //    if (playerMovement != null)
            //    {
            //        playerMovement.enabled = false;
            //    }
            //    PlayerAttack playerAttack = GetComponent<PlayerAttack>();
            //    if (playerAttack != null)
            //    {
            //        playerAttack.enabled = false;
            //    }
            //    body.velocity = Vector2.zero;
            //        body.freezeRotation = true;
            //        dead = true;
            //        Invoke("Respawn", 10f);
            //    }
            }
        }

    // RPC Calls to synchronise hrut and dead animations to all plauers
    [PunRPC]
    private void PlayHurtAnimation()
    {
        anim.SetTrigger("Hurt");
    }

    [PunRPC]
    private void PlayDeadAnimation()
    {
        // play death animation 
        anim.SetTrigger("Dead");
        /** stop all player movement and invoke respawn function
        for all players **/
        body.velocity = Vector2.zero;
        body.freezeRotation = true;
        dead = true;
        Invoke("Respawn", 10f); // Respawn after a delay
    }


    public void TakeEnemyDamage(float damage)
    {

        {
            health = Mathf.Clamp(health - damage, 0, playerHealth);
            if (health > 0)
            {
                StartCoroutine(Invunrability());
            }
            else
            {
                if (!dead)
                {
                    body.velocity = Vector2.zero;
                    body.freezeRotation = true;
                    dead = true;
                    photonView.RPC("EnemyDied", RpcTarget.All);

                    if (GetComponent<EnemyMovement>() != null)
                    {
                        GetComponentInParent<EnemyMovement>().enabled = false;
                    }

                    if (GetComponent<MeleeEnemy>() != null)
                    {
                        GetComponent<MeleeEnemy>().enabled = false;
                    }
                }
            }
        }
    }

    [PunRPC]
    private void EnemyDied()
    {
        gameObject.SetActive(false);
        Debug.Log("RPC called");
        if (Rewards != null)
        {
            Debug.Log("Boss defeated, activating rewards.");
            Rewards.SetActive(true);
        }
    }


    // Function to respawn player
    void Respawn()
    {
    
       DieUi.SetActive(false); // Deactivate UI element
       dead = false; // set dead to false
       anim.SetTrigger("Jump"); // jump animation for player to get back up
       GetComponent<PlayerMovement2D>().enabled = true; // enable movement
       playerHealth = 2f; // Reset player health
       GetComponent<PlayerAttack>().enabled = true;
    }
    // Function to add health to player
    public void AddHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, playerHealth);
    }
    // Coroutine for invulnerability frames
    private IEnumerator Invunrability()
    {
        
        Physics2D.IgnoreLayerCollision(10,11,true); // Ignore collision between player and enemy layers
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f); // set player sprite to red
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(IFrameDuration / (numberOfFlashes * 2));
        }    
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    internal bool TakeEnemyDamage()
    {
        throw new NotImplementedException();
    }

}
