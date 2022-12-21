using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCross : Watcher
{
    [SerializeField] SimpleCrosswalk simpleCrosswalk;
    protected override void Awake()
    {
        //
        if (simpleCrosswalk == null)
            Debug.Log("Назначь crosswalk этому watcher'у");
        simpleCrosswalk.ChangeStatus += ResetWatcher;
        return;
    }

    protected override void Start()
    {
        base.Start();
        di = DI.instance;
        di.tooltip.ChangeTooltipText("Перед переходом через переход обязательно нужно посмотреть по сторонам.");
    }

    protected override bool OtherPeopleCanGo()
    {
        //тэг перехода равен Player
        if (simpleCrosswalk.CompareTag("Player"))
            return true;
        else
            return false;
    }

    protected override void SemaphoreWarningOn()
    {
        di.tooltip.ChangeTooltipText("Перед переходом через переход обязательно нужно посмотреть по сторонам.");
        dieZone.SetActive(true);
    }
}

