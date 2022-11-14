using UnityEngine;

public class Bicycle : MonoBehaviour
{

	public Animator Animator;
	public string TriggerStopBicycle = "Stop";
	public string TriggerOnBicycle = "OnBicycle";

	public void StopBicycle()
	{
		Animator.SetTrigger(TriggerStopBicycle);
	}
	public void OnBicycle()
	{
		Animator.SetTrigger(TriggerOnBicycle);
	}
}
