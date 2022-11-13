using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject tipGameObject;
    [SerializeField] Text tooltipText;
    [SerializeField] InputActionReference controllerTipButton;

    public void ChangeTooltipText(string value)
    {
        tooltipText.text = value;
    }

    private void OnEnable()
    {
        if (tipGameObject == null)
            Debug.Log("Назначь подсказку объекту " + gameObject.name);

        if (controllerTipButton == null)
            Debug.Log("Назначь кнопку для подсказки на " + gameObject.name);

        controllerTipButton.action.performed += TipPress;
    }
    private void OnDisable()
    {
        controllerTipButton.action.performed -= TipPress;
    }
    private void TipPress(InputAction.CallbackContext obj)
    {
        tipGameObject.SetActive(!tipGameObject.activeSelf);
    }


}
