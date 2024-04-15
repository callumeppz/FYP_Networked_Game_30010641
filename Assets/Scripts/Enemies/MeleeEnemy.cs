using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviourPun
{

    /// <summary>
    /// This script took inspiration from the youtube video by Pandemonium released in 2022. 
    /// https://www.youtube.com/watch?v=d002CljR-KU&t=1950s&ab_channel=Pandemonium
    /// </summary>


    // Variables for attack cooldown, damage, and cooldown timer initialised
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;

    // Collider and layer mask variables initialised
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    private Animator anim;
    private PlayerHealth playerHealth;
    private EnemyMovement enemyMovement;
    private Rigidbody body;

      /** 
      * Start method is called before the first frame update.
      * It gets necessary components.
      */

    private void Start()
    {
        // Getying components
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>(); 
        body = GetComponent<Rigidbody>();
    }

     /** 
     * Update method is called once per frame.
     * It updates the attack cooldown timer and checks if the player is in sight
     */

    void Update()
    {
        // Update cooldown timer
        cooldownTimer = Time.deltaTime;
        // Check if player is in sight and if cooldown is over
        if (PlayerInSight())
        {
            if (cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                //collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                // Attack player animation started
                anim.SetTrigger("isAttacking");
            }
        }
    }

    // Method triggered when pklayer enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
       {
            if (collision.tag == "Player")
           {
            // damage player and enable attack animation
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                anim.SetTrigger("isAttacking");
                body.isKinematic = true;
                
            }
        }
    // Method to check if player is in sight
    private bool PlayerInSight()
    {
        // Cast a box in front of the enemy to detect the player
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        // If detected, get player's health
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }
    // Method to illustrate gizmos or attack range to the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    // method used for player damage
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
