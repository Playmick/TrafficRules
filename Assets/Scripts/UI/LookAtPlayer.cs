using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// для подсказок, которые должны вразаться по оси y, постоянно смотря на игрока
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
    Transform player;
    public Transform followObj;
    public float offset;
    float speed=15;
    public bool lockY=true;
    public bool offOnStart = true;

    private void Start()
    {
        if (offOnStart)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(player == null)
        {
            StartCoroutine(FindPlayer());
        }
    }

    IEnumerator FindPlayer()
    {
        while (player == null)
        {
            player = GameObject.Find("Main Camera").transform;
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (followObj != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(followObj.transform.position.x, followObj.transform.position.y + offset, followObj.transform.position.z), speed * Time.deltaTime);
        }
        transform.LookAt(player.transform.position);
        if (lockY)
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles[1], 0);
    }

}
