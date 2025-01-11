using UnityEngine;
using UnityEngine.Events;

public class SoundOnClick : Interactable
{
	public UnityEvent eventToTrigger;
    public override void InteractStart()
	{
		eventToTrigger?.Invoke();
	}
}
