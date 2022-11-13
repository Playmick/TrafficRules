using UnityEngine;

public class ZoneColor: MonoBehaviour
{
    public Watcher watcher;
    public MainZone mainZone;
    private bool playerLookedToThis = false;

    private void Awake()
    {
        if (watcher == null)
            Debug.Log("Этой палитре нужна ссылка на watcher");
        if (mainZone == null)
            Debug.Log("Этой палитре нужна ссылка на MainZone");
    }

    public bool PlayerLookedToThis
    {
        get => playerLookedToThis;
        set
        {
            playerLookedToThis = value;
            ChangeColor();
        }
    }

    private void ChangeColor(Material mat = null)
    {
        if (mat == null)
            mat = watcher._correctMat;

        if(playerLookedToThis)
        {
            gameObject.GetComponent<MeshRenderer>().material = mat;
            watcher.CheckWatcher();
        }
        else
            gameObject.GetComponent<MeshRenderer>().material = watcher._inCorrectMat;
    }

}
