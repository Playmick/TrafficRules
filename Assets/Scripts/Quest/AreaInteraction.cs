using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AreaInteraction : Interactive
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsCompleted = true;
        }
    }
}
