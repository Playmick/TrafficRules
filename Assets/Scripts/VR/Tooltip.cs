using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject tipGameObject;
    [SerializeField] Text tooltipText;
    [SerializeField] Text closeText;
    [SerializeField] InputActionReference controllerTipButton;

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
            SceneManager.LoadScene("MainMenu");
    }
    public void ResetTime()
    {
        time=3;
        UpdateCloseText();
    }
    public void UpdateCloseText()
    {
        closeText.text = $"Удерживайте курок контроллера {time} секунды для выхода из сценария";
    }


    private void OnEnable()
    {
        time = 3;

        if (tipGameObject == null)
            Debug.Log("Назначь подсказку объекту " + gameObject.name);

        if (controllerTipButton == null)
            Debug.Log("Назначь кнопку для подсказки на " + gameObject.name);

        if (closeText == null)
            Debug.Log("Назначь текст закрытия объекту " + gameObject.name);

        closeText.text = "";
        canClose = true;

        controllerTipButton.action.performed += TipPress;
    }
    private void OnDisable()
    {
        controllerTipButton.action.performed -= TipPress;
    }
    private void TipPress(InputAction.CallbackContext obj)
    {
        if(canClose)
            tipGameObject.SetActive(!tipGameObject.activeSelf);
    }
}
