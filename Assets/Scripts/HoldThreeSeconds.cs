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

    private void Awake()
    {
        activateButton.action.performed += HoldActivate;
        deActivateButton.action.canceled += HoldCanceled;
    }
    private void HoldActivate(InputAction.CallbackContext obj)
    {
        //Debug.Log("����� ����������");
        //hold = true;
        routine = HoldOn();
        StartCoroutine(routine);
            
    }

    IEnumerator HoldOn()
    {
        while(true)
        {
            //Debug.Log("������");
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
