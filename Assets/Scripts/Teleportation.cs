using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject destination;
    private bool canBeUsed;
    [SerializeField] private float timeBetweenUse;

    private void Start()
    {
        canBeUsed = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (canBeUsed)
        {
            GameObject player = GameObject.Find("Player");

            if (player.GetComponent<PlayerTeleportationManagement>().teleportationActive)
            {
                player.GetComponent<PlayerTeleportationManagement>().ManageCooldown();
                player.GetComponent<Transform>().position = destination.GetComponent<Transform>().position;
                GetComponent<AudioSource>().Play();
                DeaktivatePortal(destination);
            }
        }
    
    }

    private void DeaktivatePortal(GameObject portal)
    {
        portal.transform.GetComponent<Teleportation>().canBeUsed = false;
        StartCoroutine(TimeBetweenBlackHoleAction(portal));

    }

    private IEnumerator TimeBetweenBlackHoleAction(GameObject portal)
    {
        while (!portal.transform.GetComponent<Teleportation>().canBeUsed)
        {
            yield return new WaitForSeconds(timeBetweenUse);
            
            portal.transform.GetComponent<Teleportation>().canBeUsed = true;
        }
    }
}
