using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviourPun
{

    /// <summary>
    /// A work in progress health script with inspirtaion from Pandemonium in 2021
    /// https://www.youtube.com/watch?v=yxzg8jswZ8A&t=227s&ab_channel=Pandemonium
    /// </summary>

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerHealth = GetComponent<PlayerHealth>();
            UpdateHealthBar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) // checking if the photonview belongs to the correct player
        {
            UpdateHealthBar();
        }
    }
    // method to update players healthbar
    void UpdateHealthBar()
    {
        float fillAmount = playerHealth.health / 10f;
        totalHealthBar.fillAmount = playerHealth.health / 10;
        currentHealthBar.fillAmount = playerHealth.health / 10;

        photonView.RPC("UpdateHealthBarRPC", RpcTarget.AllBuffered, fillAmount);
    }

    // RPC call to inform all players of healthbar change, held in cache for new players
    [PunRPC]
    void UpdateHealthBarRPC(float fillAmount)
    {
        totalHealthBar.fillAmount = fillAmount;
        currentHealthBar.fillAmount = fillAmount;
    }
}

