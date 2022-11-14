using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCrosswalk : MonoBehaviour
{
    private bool havePeople;
    public event EventHandler ChangeStatus;

    public bool HavePeople
    {
        get
        {
            return havePeople;
        }
        set
        {
            havePeople = value;
            ChangeStatus?.Invoke(this, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            HavePeople = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            HavePeople = false;
    }
}
