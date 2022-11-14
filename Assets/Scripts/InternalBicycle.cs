using UnityEngine;

public class InternalBicycle : MonoBehaviour
{

	public Animator Animator;
	public string TriggerName = "GoForward";

	public void GoForward()
	{
		Animator.SetTrigger(TriggerName);
	}
}
