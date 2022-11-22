using UnityEngine;
using UnityEngine.Events;

public class OnAwakeTrigger : MonoBehaviour
{
	public UnityEvent OnEnableEvent;

	private void OnEnable()
	{
		OnEnableEvent.Invoke();
	}
}
