using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCar : MonoBehaviour
{
    bool stop = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!stop)
            transform.Translate(0f, 0f, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SimpleCrosswalk>().HavePeople)
        {
            stop = true;
            
        }
        other.GetComponent<SimpleCrosswalk>().ChangeStatus += ChangeStopState;
    }

    private void ChangeStopState(object sender, EventArgs e)
    {
        stop = ((SimpleCrosswalk)sender).HavePeople;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SimpleCrosswalk>().HavePeople)
        {
            other.GetComponent<SimpleCrosswalk>().ChangeStatus -= ChangeStopState;
        }

    }
}
