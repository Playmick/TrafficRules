using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCounter : MonoBehaviour
{
    private bool havePeople;
    public Action ChangeStatus;

    public int HavePeople { get; set; }

    private void Start()
    {
        HavePeople = 0;
        transform.gameObject.tag = "Untagged";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("People"))
        {
            if (HavePeople==0)
                ChangeStatus?.Invoke();

            HavePeople++;
            transform.gameObject.tag = "Player";
            //просто потому что на плеера автомобили останавливаются
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("People"))
        {
            HavePeople--;
            if(HavePeople<=0)
            {
                transform.gameObject.tag = "Untagged";
                HavePeople = 0;
                ChangeStatus?.Invoke();
            }
                
        }
    }
}
