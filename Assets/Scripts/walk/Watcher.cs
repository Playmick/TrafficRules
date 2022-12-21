using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    // лист направлений(их всего 2)
    // в каждом направлении 2 стороны чтобы посмотреть налево и направо
    public Direction[] directions;

    protected DI di;

    public SemaphorePeople semaphore;
    [SerializeField] protected GameObject ColorBoxCrosswalk;
    [SerializeField] protected GameObject dieZone;

    protected virtual void Awake()
    {
        semaphore.ChangeLightColor += ResetWatcher;

        CheckAllRight();
    }

    protected virtual void Start()
    {
        di = DI.instance;
        foreach (Direction dir in directions)
        {
            foreach (ZoneColor zc in dir.playerLooked)
            {
                zc.OnPlayerLookedToThis += CheckWatcher;
            }
        }
    }

    //выполнялся когда игрок посмотрел на него
    //нужно подписать событием
    public virtual void CheckWatcher()
    {
        if(ManLookedAround())
        {
            ColorBoxCrosswalk.GetComponent<MeshRenderer>().material = di._correctMat;
            dieZone.SetActive(false);
        }
    }

    private void CheckAllRight()
    {
        if (ColorBoxCrosswalk == null)
            Debug.Log("Прокинь сюда ссылку на ColorBoxCrosswalk");

        if (directions.Length <= 1)
            Debug.Log("Заполни массив directions в Watcher, помести туда зоны в которые должен посмотреть игрок.");

        if (semaphore == null)
            Debug.Log("Прокинь сюда ссылку на semaphore");
    }

    protected virtual bool ManLookedAround()
    {
        foreach (Direction dir in directions)
        {
            if (dir.mainZone.playerStayInZone)
            {
                //если игрок стоит в зоне
                if (OtherPeopleCanGo() == false)
                    return false;

                foreach (ZoneColor zc in dir.playerLooked)
                {
                    if (zc.PlayerLookedToThis == false)
                        return false;
                }
                return true;
            }
        }
        return false;
    }

    protected virtual bool OtherPeopleCanGo()
    {
        return semaphore.PEOPLE_CAN;
    }

    public void ResetWatcher()
    {
        if (OtherPeopleCanGo())
        {
            di.tooltip.ChangeTooltipText("Перед переходом через переход обязательно нужно посмотреть по сторонам.");
        }
        if (OtherPeopleCanGo() == false)
        {
            ResetZones();
            SemaphoreWarningOn();
        }

    }

    protected virtual void SemaphoreWarningOn()
    {
        di.tooltip.ChangeTooltipText("Вы перешли на красный сигнал светофора!");
        StartCoroutine(OnDieZone());
    }

    private void ResetZones()
    {
        //все зоны во всех массивах направления сделать красным
        foreach (Direction dir in directions)
        {
            foreach (ZoneColor zc in dir.playerLooked)
            {
                zc.PlayerLookedToThis = false;
            }
        }

        ColorBoxCrosswalk.GetComponent<MeshRenderer>().material = di._inCorrectMat;
    }

    IEnumerator OnDieZone()
    {
        yield return new WaitForSeconds(3f);
        dieZone.SetActive(true);
    }
}

[System.Serializable]
public class Direction
{
    public ZoneColor[] playerLooked;
    public MainZone mainZone;
}

