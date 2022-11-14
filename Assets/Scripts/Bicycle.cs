using UnityEngine;

public class Bicycle : MonoBehaviour
{

	public Animator Animator;
	public string TriggerName = "Stop";

	public void StopBicycle()
	{
		Animator.SetTrigger(TriggerName);
	}
}
