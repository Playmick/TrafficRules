using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zone")
        {
            ZoneColor zone = other.gameObject.GetComponent<ZoneColor>();

            if (zone.watcher.semaphore.PEOPLE_CAN && zone.mainZone.playerStayInZone)
            {
                zone.PlayerLookedToThis = true;
            }
        }
            
    }
}
