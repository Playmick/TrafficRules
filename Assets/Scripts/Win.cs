using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private DI di;

    private string winText = "Выполнено";
    public GameObject arrow;

    public string WinText { get => winText; set => winText = value; }

    //bool tablNotSubscribe;
    void Start()
    {
        di = DI.instance;
        //tablNotSubscribe = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinMethod();
        }
    }

    public void WinMethod()
    {
        if (arrow != null)
            arrow.SetActive(false);
        //сменить текст на табличке
        di.tooltip.ChangeTooltipText(WinText);
        di.tooltip.UpdateCloseText();
        //di.tooltip.ChangeImage(di.Svet2);
        //включить табличку
        di.tooltip.ShowTip();

        di.tooltip.EndOfButtonHold += LoadMainMenu;
        /*
        //подписываем табличку к событию
        if (tablNotSubscribe)
        {
            di.holdThreeSeconds.ReduceTime += di.tooltip.ReduceTime;
            di.holdThreeSeconds.ResetTime += di.tooltip.ResetTime;
            tablNotSubscribe = false;
        }*/

        //отключить телепорт
        di.rightTeleportController.SetActive(false);
        di.leftTeleportController.SetActive(false);
    }

    private void LoadMainMenu()
    {
        di.tooltip.EndOfButtonHold -= LoadMainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}
