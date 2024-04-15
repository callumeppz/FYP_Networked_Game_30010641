using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SlashProjectile : MonoBehaviourPun
{

    [SerializeField] private float speed; // speed of projectile 
    private bool hit; // bool determining if hit
    private float direction; // directiomn of projectile
    private float lifeTIme; // how long the projectile lasts
    [SerializeField] public int damage = 1; // amount of damage projectile does at default

    private BoxCollider2D boxCollider; // getting necessary components
    private Animator anim;

    // Start is called before the first frame update
    void Start() // fetching neccesary components
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return; // Exit Update if the projectile has already hit something
        float invertedDirection = -direction;
        float movementSpeed = speed * Time.deltaTime * invertedDirection;
        transform.Translate(movementSpeed, 0, 0);

        lifeTIme += Time.deltaTime;
        if(lifeTIme > 5) {
            gameObject.SetActive(false);  // Deactivate the projectile after 5 seconds
        }

    }
    // on hit deactivate and take damage variable from enemy health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.name);
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Hit");

        if (collision.CompareTag("EnemyCrab"))
        {
            collision.GetComponent<PlayerHealth>().TakeEnemyDamage(damage);
            Debug.Log("EnemyHit");
        }
    }

    // Set the direction of the projectile and activate it
    public void SetDirection(float _direction)
    {
        lifeTIme = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    // deactivate projectile
    private void Deactivate ()
    {
        gameObject.SetActive(false);
    }
    // Increase the damage of the projectile
    public void IncreaseDamage()
    {
        damage += 1;
    }
}
