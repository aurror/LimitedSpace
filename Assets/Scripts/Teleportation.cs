using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject destination;

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject player = GameObject.Find("Player");

        if (player.GetComponent<PlayerTeleportationManagement>().teleportationActive)
        {
            player.GetComponent<PlayerTeleportationManagement>().ManageCooldown();
            player.GetComponent<Transform>().position = destination.GetComponent<Transform>().position;
        }
    }
}
