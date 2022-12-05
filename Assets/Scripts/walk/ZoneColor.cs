using UnityEngine;
using System;

public class ZoneColor: MonoBehaviour
{
    public Watcher watcher;
    public MainZone mainZone;
    public Action OnPlayerLookedToThis;
    public bool PlayerLookedToThis
    {
        get => playerLookedToThis;
        set
        {
            if (value == false)
            {
                playerLookedToThis = value;
                ChangeColor();
            }
            else
            {
                if (watcher.semaphore != null)
                {
                    if (watcher.semaphore.PEOPLE_CAN && mainZone.playerStayInZone)
                    {
                        playerLookedToThis = value;
                        ChangeColor();
                    }
                }
                else
                {
                    if (mainZone.playerStayInZone)
                    {
                        playerLookedToThis = value;
                        ChangeColor();
                    }
                }
                OnPlayerLookedToThis?.Invoke();
            }
        }
    }

    private bool playerLookedToThis;

    private void Awake()
    {
        playerLookedToThis = false;
        if (watcher == null)
            Debug.Log("Этой палитре нужна ссылка на watcher");
        if (mainZone == null)
            Debug.Log("Этой палитре нужна ссылка на MainZone");
    }
    

    private void ChangeColor(Material mat = null)
    {
        if (mat == null)
            mat = DI.instance._correctMat;

        if(playerLookedToThis)
        {
            gameObject.GetComponent<MeshRenderer>().material = mat;
            watcher.CheckWatcher();
        }
        else
            gameObject.GetComponent<MeshRenderer>().material = DI.instance._inCorrectMat;
    }

}
