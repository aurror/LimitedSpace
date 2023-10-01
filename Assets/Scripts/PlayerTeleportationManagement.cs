using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportationManagement : MonoBehaviour
{
    public float teleportationCoolDown;
    public bool teleportationActive = true;

    public void ManageCooldown()
    {
        teleportationActive = false;
        Invoke("ActivateTeleportation", teleportationCoolDown);
    }

    void ActivateTeleportation()
    {
        teleportationActive = true;
    }
}
