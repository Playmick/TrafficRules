using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : ShowMessageInTrigger
{
    protected override void MainMethod()
    {
        base.MainMethod();

        //"вспышка"
        di.blackColor.SetActive(true);
        di.flashBlindness.SetActive(true);

        //включаем звук
        di.dieSound?.Play();
    }
}
