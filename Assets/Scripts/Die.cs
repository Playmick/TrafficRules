using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    protected DI di;
    //[SerializeField] bool defaultText;

    bool tablNotSubscribe;
    void Start()
    {
        di = DI.instance;
        tablNotSubscribe = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            /*
            //сменить текст на табличке
            if(defaultText)
                di.tooltip.ChangeTooltipText("Нельзя выходить на дорогу, нарушая правила дорожного движения.");
            */

            //"вспышка"
            di.blackColor.SetActive(true);
            di.flashBlindness.SetActive(true);

            di.tooltip.ChangeTooltipText("Переход не безопасен. Ознакомьтесь с ПДД 4.5");
            di.tooltip.CloseImage();
            di.tooltip.UpdateCloseText();
            //включить табличку
            di.tooltip.ShowTip();

            di.tooltip.EndOfButtonHold += LoadMainMenu;
            /*
            //подписываем табличку к событию
            if(tablNotSubscribe)
            {
                di.holdThreeSeconds.ReduceTime += di.tooltip.ReduceTime;
                di.holdThreeSeconds.ResetTime += di.tooltip.ResetTime;
                tablNotSubscribe = false;
            }*/


            //включаем звук
            di.dieSound.Play();

            //отключить телепорт
            di.rightTeleportController.SetActive(false);
            di.leftTeleportController.SetActive(false);
        }
    }

    private void LoadMainMenu()
    {
        di.tooltip.EndOfButtonHold -= LoadMainMenu;
        SceneManager.LoadScene("MainMenu");
    }
}
