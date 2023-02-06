using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldThreeSeconds : MonoBehaviour
{
    public InputActionReference activateButton;
    public InputActionReference deActivateButton;

    public Action ReduceTime;
    public Action ResetTime;

    private int time;

    //private bool hold;

    private IEnumerator routine;

    private void OnEnable()
    {
        activateButton.action.performed += HoldActivate;
        deActivateButton.action.canceled += HoldCanceled;
    }

    private void OnDisable()
    {
        activateButton.action.performed -= HoldActivate;
        deActivateButton.action.canceled -= HoldCanceled;
    }

    private void HoldActivate(InputAction.CallbackContext obj)
    {
        //Debug.Log("Начал удерживать");
        //hold = true;
        routine = HoldOn();
        StartCoroutine(routine);
            
    }

    IEnumerator HoldOn()
    {
        while(true)
        {
            //Debug.Log("Держим");
            yield return new WaitForSeconds(1f);
            ReduceTime?.Invoke();
        }
    }

    private void HoldCanceled(InputAction.CallbackContext obj)
    {
        //hold = false;
        StopCoroutine(routine);
        ResetTime?.Invoke();
    }

    
}
