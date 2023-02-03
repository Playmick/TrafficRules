using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject tipGameObject;
    [SerializeField] Text tooltipText;
    [SerializeField] Text closeText;
    [SerializeField] InputActionReference controllerTipButton;
    [SerializeField] Image Image;

    public Action EndOfButtonHold;

    DI di;

    private bool canClose;
    [SerializeField] private int time;

    public void ChangeTooltipText(string value)
    {
        if(!tipGameObject.activeSelf)
            tooltipText.text = value;
    }
    public void ShowTip()
    {
        tipGameObject.SetActive(true);
        di.holdThreeSeconds.ReduceTime += di.tooltip.ReduceTime;
        di.holdThreeSeconds.ResetTime += di.tooltip.ResetTime;
        canClose = false;
    }
    public void CloseTip()
    {
        tipGameObject.SetActive(false);
        canClose = true;
    }
    public void ReduceTime()
    {
        time = time-1;
        UpdateCloseText();
        if (time <= 0)
        {
            EndOfButtonHold?.Invoke();
            tipGameObject.SetActive(false);
        }
            
    }
    public void ResetTime()
    {
        time=3;
        UpdateCloseText();
    }

    public void UpdateCloseText()
    {
        closeText.text = $"����������� ����� ����������� {time} ������� ��� ������ �� ��������";
    }

    public void ChangeImage(Sprite _sprite)
    {
        Image.enabled = true;
        Image.sprite = _sprite;
    }


    private void OnEnable()
    {
        di = DI.instance;
        time = 3;

        if (tipGameObject == null)
            Debug.Log("������� ��������� ������� " + gameObject.name);

        if (controllerTipButton == null)
            Debug.Log("������� ������ ��� ��������� �� " + gameObject.name);

        if (closeText == null)
            Debug.Log("������� ����� �������� ������� " + gameObject.name);

        if (Image == null)
            Debug.Log("������� �������� ������� " + gameObject.name);

        closeText.text = "";
        canClose = true;

        controllerTipButton.action.performed += TipPress;
    }
    private void OnDisable()
    {
        controllerTipButton.action.performed -= TipPress;
        di.holdThreeSeconds.ReduceTime -= di.tooltip.ReduceTime;
        di.holdThreeSeconds.ResetTime -= di.tooltip.ResetTime;
    }
    private void TipPress(InputAction.CallbackContext obj)
    {
        if(canClose)
            tipGameObject.SetActive(!tipGameObject.activeSelf);
    }
}
