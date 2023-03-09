using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchInteraction : Interactive
{
    [SerializeField] ZoneColor zoneColor;

    private void OnEnable()
    {
        zoneColor.OnPlayerLookedToThis += OnComplete;
    }

    private void OnDisable()
    {
        zoneColor.OnPlayerLookedToThis -= OnComplete;
    }

    private void OnComplete()
    {
        IsCompleted = true;
    }
}
