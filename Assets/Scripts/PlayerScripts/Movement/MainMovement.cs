using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement2D : MonoBehaviourPun
{

    /// <summary>
    /// A large majority of this script was influenced by Pandemoniums movement video on YouTube from 2021
    /// https://www.youtube.com/watch?v=TcranVQUQ5U&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&ab_channel=Pandemonium
    /// </summary>

    // variable insitalisation 
    [Header("Player Physics")]
    private Rigidbody2D body;
    private Animator anim;
    private Collision coll;
    public Text PlayerNameText;
    PhotonView photonView;

    private bool isFacingRight = true;
    [SerializeField] private float speed;
    [SerializeField] public float jumpingPower;
    [SerializeField] private float jumpcount = 1;

    [Header("Collider Size")]
    bool Grounded;
    public Vector2 BoxSize;
    public float castDistance;
    public Collider2D boxCollider;
    public LayerMask GroundLayer;
    public LayerMask WallLayer;

    [Header("Wall Physics")]
    private float wallJumpCooldown;
    [SerializeField] float defaultFriction = 0f;
    [SerializeField] float wallSlideFriction = 0.5f;
    public int maxWallJumps = 3; 
    private int wallJumpCount = 0;




    void Start()
    {
        // Initialise components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        photonView = GetComponent<PhotonView>();
        // Set player name text
        if (photonView.IsMine)
        {
            PlayerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerNameText.text = photonView.Owner.NickName;
        }

        photonView.OwnershipTransfer = OwnershipOption.Fixed;
    }


    void Update()
    {
        if (photonView.IsMine)
        {
            // Get horizontal input
            float horizontalInput = Input.GetAxis("Horizontal");
            // move player horizontally 
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // Flip player
            FlipPlayer(horizontalInput);
            // jumping feature
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpingPower);
                Grounded = false;
                anim.SetTrigger("Jump");
            }

            if (Input.GetButtonUp("Jump") && body.velocity.y > 0f && isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
                Grounded = false;
                anim.SetTrigger("Jump");
            }
            // animation states
            anim.SetBool("isRunning", Mathf.Abs(horizontalInput) > 0f);
            anim.SetBool("isGrounded", isGrounded());
            anim.SetBool("isWalled", isWalled());
            // wall jumping
            if (isWalled())
            {
                if (Input.GetButtonDown("Jump") && wallJumpCount < maxWallJumps)
                {
                    body.velocity = new Vector2(-transform.localScale.x * speed, jumpingPower);
                    Grounded = false;
                    anim.SetTrigger("Jump");
                    wallJumpCount++; 
                }
            }
            else if (isGrounded())
            {
                wallJumpCount = 0;
            }

                print(isWalled());

        }
    }
    // Flip the player based on horizontal input
    void FlipPlayer(float horizontalInput)
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            if (photonView.IsMine)
            {
                photonView.RPC("SyncFlip", RpcTarget.Others, isFacingRight);
            }
        }
    }

    // sync player flip across the network
    [PunRPC]
    void SyncFlip(bool facingRight)
    {
        isFacingRight = facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = facingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    // Check if the player is grounded
    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, BoxSize, 0, -transform.up, castDistance, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Check if the player is touching a wall
    public bool isWalled()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(transform.position, BoxSize, 0, Vector2.left, castDistance, GroundLayer);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(transform.position, BoxSize, 0, Vector2.right, castDistance, GroundLayer);
        return raycastHitLeft.collider != null || raycastHitRight.collider != null;


    }
    // Draw gizmos for debugging
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, BoxSize);
    }
    // Check if the player can attack
    public bool canAttack()
    {
        return isGrounded() && !isWalled();
    }
}

 