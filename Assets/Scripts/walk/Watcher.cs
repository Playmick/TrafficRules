using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{

    // лист объектов
    public Direction[] directions;

    
    public Material _correctMat;
    public Material _inCorrectMat;
    public SemaphorePeople semaphore;
    [SerializeField] private GameObject crosswalk;

    private void Awake()
    {
        semaphore.ChangeLightColor += ResetWatcher;
    }

    public void CheckWatcher()
    {
        if (crosswalk == null)
            Debug.Log("ѕрокинь сюда ссылку на crosswalk");

        if (directions.Length <= 1)
            Debug.Log("«аполни массив directions в Watcher, помести туда зоны в которые должен посмотреть игрок.");

        if(semaphore == null)
            Debug.Log("ѕрокинь сюда ссылку на semaphore");

        foreach (Direction dir in directions)
        {
            if (dir.mainZone.playerStayInZone)
            {
                //если игрок стоит в зоне
                if (semaphore.PEOPLE_CAN == false)
                    return;

                foreach (ZoneColor zc in dir.playerLooked)
                {
                    if (zc.PlayerLookedToThis == false)
                        return;
                }
            }
        }

        crosswalk.GetComponent<MeshRenderer>().material = _correctMat;
    }

    public void ResetWatcher()
    {
        if(semaphore.PEOPLE_CAN == false)
        {
            //все зоны во всех массивах направлени€ сделать красным
            foreach (Direction dir in directions)
            {
                foreach (ZoneColor zc in dir.playerLooked)
                {
                    zc.PlayerLookedToThis = false;
                }
            }

            crosswalk.GetComponent<MeshRenderer>().material = _inCorrectMat;
        }
        
    }
}

[System.Serializable]
public class Direction
{
    public ZoneColor[] playerLooked;
    public MainZone mainZone;
}

