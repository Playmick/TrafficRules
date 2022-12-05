using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI : MonoBehaviour
{
    #region DI
    public static DI instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion

    public Tooltip tooltip;
    public HoldThreeSeconds holdThreeSeconds;
    public AudioSource dieSound;
    public GameObject rightTeleportController;
    public GameObject leftTeleportController;
    public Material _correctMat;
    public Material _inCorrectMat;
}
