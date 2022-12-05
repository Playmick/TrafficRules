using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    protected DI di;
    [SerializeField] bool defaultText;

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
            //сменить текст на табличке
            if(defaultText)
                di.tooltip.ChangeTooltipText("Нельзя выходить на дорогу, нарушая правила дорожного движения.");
            di.tooltip.UpdateCloseText();
            //включить табличку
            di.tooltip.ShowTip();

            //подписываем табличку к событию
            if(tablNotSubscribe)
            {
                di.holdThreeSeconds.ReduceTime += di.tooltip.ReduceTime;
                di.holdThreeSeconds.ResetTime += di.tooltip.ResetTime;
                tablNotSubscribe = false;
            }
            

            //включаем звук
            di.dieSound.Play();

            //отключить телепорт
            di.rightTeleportController.SetActive(false);
            di.leftTeleportController.SetActive(false);
        }
    }
}
