using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviourPun
{
    [SerializeField]private float attackCooldown; // cooldown between attacks
    [SerializeField] private Transform slashPoint; // point of attack
    [SerializeField] private GameObject[] slashs; // array of slashes

    private Animator anim; // refereance to secific component
    private PlayerMovement2D playerMovement; // refereance to secific component
    private float cooldownTimer = Mathf.Infinity; // timer for attack cooldown

    private void Start()
    {
        SlashProjectile slashComponent = FindObjectOfType<SlashProjectile>(); // refereance to secific component in hierachy
        anim = GetComponent<Animator>(); // refereance to secific component
        playerMovement = GetComponent<PlayerMovement2D>();// refereance to secific component

        if (slashComponent != null)
        {
            slashs[0] = slashComponent.gameObject; // sassign first slash game object if found
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {// if photonview is yours, check for attack input and cooldown
            if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            {
                Attack();
            }

            cooldownTimer += Time.deltaTime;
        }
    }

    private void Attack()
    {
        // attack animation
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        // position and direction of the slash projectile
        slashs[0].transform.position = slashPoint.position;
        slashs[0].GetComponent<SlashProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    // Find an available slot in the slashs array
    private int findSlash ()
    {
        for (int i = 0; i < slashs.Length; i++)
        {
                if (!slashs[i].activeInHierarchy)
                {
                    return i;
                }
            
        }
        return 0;
    }
}
