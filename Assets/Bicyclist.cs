using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicyclist : MonoBehaviour
{
    [SerializeField] float defaultRideSpeed = 1;
    [SerializeField] float defaultWalkSpeed = 1;
    [SerializeField] Animator anim;

    public Point[] points;
    int NextNumberPoint { get; set; }
    int PrevNumberPoint { get; set; }

    float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        PrevNumberPoint = 0;
        NextNumberPoint = 0;
        speed = points[NextNumberPoint].speedForThisPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(NextNumberPoint < points.Length)
            transform.position = Vector3.MoveTowards(transform.position, points[NextNumberPoint].transform.position, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CyclePoint"))
        {
            PrevNumberPoint = NextNumberPoint;
            NextNumberPoint++;
            anim.Play(points[PrevNumberPoint].animationTrigger);
            transform.rotation = other.transform.rotation;
            speed = points[PrevNumberPoint].speedForThisPoint;
            if (points[PrevNumberPoint].animationTrigger == "Offbicycle" || points[PrevNumberPoint].animationTrigger == "Onbicycle")
                StartCoroutine(StartMoveAfterStop());
        }
    }
    IEnumerator StartMoveAfterStop()
    {
        yield return new WaitForSeconds(1f);
        speed = defaultRideSpeed;
        if (points[PrevNumberPoint].animationTrigger == "Offbicycle")
        {
            anim.Play("Walk");
            speed = defaultWalkSpeed;
        } 
        else
            anim.Play("RideLoop");

    }
}



[System.Serializable]
public class Point
{
    public Transform transform;
    public string animationTrigger;
    public float speedForThisPoint;
}
