using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollectable : MonoBehaviour
{
    // initialise variables
    public SlashProjectile attackDmg;
    /** Start is called before the first frame update, and utilised to find SlashProjectile component in hiearchy
     */
    private void Start()
    {
        attackDmg = FindObjectOfType<SlashProjectile>();
    }

    /// <summary>
    /// Checks if collision with player, if so player collects powerup and attack damage is increased, thus the powerup is set to inactive
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            attackDmg.IncreaseDamage(); // initiate increasedamage method in attackplayer script
        }
    }
}

