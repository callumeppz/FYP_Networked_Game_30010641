using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MOVEMENT : MonoBehaviourPun
{
    /// <summary>
    /// partly based on a video realeased by PantsOnLave from 2023
    /// https://www.youtube.com/watch?v=34fgsJ2-WzM&ab_channel=PantsOnLava
    /// </summary>


    private MovementState state;

    // initialise variables
    [Header("Player Physics")]
    private Rigidbody2D body;
    private Animator anim;
    private Collision coll;
    public Text PlayerNameText;
    PhotonView photonView;

    [Header("Player Features")]
    private bool isFacingRight = true;
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float jumpcount = 1;

    private enum MovementState
    {
        Idle,
        Running,
        Jumping
    }

    void Start()
    {
        // get components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        photonView = GetComponent<PhotonView>();

        // check if Photon is mine, then assign nicknames
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

    // RPC call to play run animation across Photon
    void Update()
    {
        if (photonView.IsMine)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float veticleInput = Input.GetAxis("Vertical");

            Vector2 movementDirection = new Vector2(horizontalInput, veticleInput).normalized;
            body.velocity = movementDirection * speed;

            // Flip player
            FlipPlayer(horizontalInput);

            photonView.RPC("PlayRunAnimation", RpcTarget.All);
            anim.SetBool("isRunning", Mathf.Abs(horizontalInput) > 0f);



            if (Input.GetKey(KeyCode.W))
            {
                anim.SetTrigger("isWalkingUp");
            }
        }
    }

    [PunRPC]
    private void PlayRunAnimation()
    {
        anim.SetBool("isRunning", true);
    }
    // Flip player horizontally
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
    // Synchronise player flip accross Photon
    [PunRPC]
    void SyncFlip(bool facingRight)
    {
        isFacingRight = facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = facingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    // Synchronise animations
    [PunRPC]
    private void SyncAnimation(int stateValue)
    {
        state = (MovementState)stateValue;
    }
}
