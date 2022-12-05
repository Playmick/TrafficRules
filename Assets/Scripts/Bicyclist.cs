using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicyclist : MonoBehaviour
{
    /// <summary>
    /// yield WaitForSeconds(animation.clip.length);
    /// ожидание пока кончится анимация
    /// </summary>

    [SerializeField] float defaultRideSpeed = 1;
    [SerializeField] float defaultWalkSpeed = 1;
    [SerializeField] Animator animator;

    public Point[] points;
    int NextNumberPoint { get; set; }
    int PrevNumberPoint { get; set; }

    float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        PrevNumberPoint = 0;
        NextNumberPoint = 0;
        speed = points[NextNumberPoint].anims[0].speedMoveObjectForThisAnimation;
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
            StartCoroutine(AnimRoutine());
            transform.rotation = other.transform.rotation;

            /*
            animator.Play(points[PrevNumberPoint].name);
            speed = points[PrevNumberPoint].speedForThisPoint;
            if (points[PrevNumberPoint].name == "Offbicycle" || points[PrevNumberPoint].name == "Onbicycle")
                StartCoroutine(StartMoveAfterStop());*/
        }
    }

    
    IEnumerator AnimRoutine()
    {
        foreach(AnimationList anim in points[PrevNumberPoint].anims)
        {
            animator.Play(anim.name);
            speed = anim.speedMoveObjectForThisAnimation;
            yield return null;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        


        /*
        speed = defaultRideSpeed;
        if (points[PrevNumberPoint].name == "Offbicycle")
        {
            anim.Play("Walk");
            speed = defaultWalkSpeed;
        } 
        else
            anim.Play("RideLoop");*/

    }
}



[System.Serializable]
public class Point
{
    public Transform transform;
    public AnimationList[] anims;
    int currentAnim = 0;
}

[System.Serializable]
public class AnimationList
{
    public string name;
    public float speedMoveObjectForThisAnimation;
}
