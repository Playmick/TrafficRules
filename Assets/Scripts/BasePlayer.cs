using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DI.instance.tooltip.ChangeTooltipText("Нельзя выходить на дорогу, нарушая правила дорожного движения.");
    }
    
}
