using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zone")
        {
            //Debug.Log("Zone checker заметил зону");
            ZoneColor zone = other.gameObject.GetComponent<ZoneColor>();

            zone.PlayerLookedToThis = true;
        }
            
    }
}
