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

    private void Awake()
    {
        activateButton.action.performed += HoldActivate;
        deActivateButton.action.canceled += HoldCanceled;
    }
    private void HoldActivate(InputAction.CallbackContext obj)
    {
        //Debug.Log("Начал удерживать");
        StartCoroutine(HoldOn());
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
        StopCoroutine(HoldOn());
        ResetTime?.Invoke();
    }

    
}
