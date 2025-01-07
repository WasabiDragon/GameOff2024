using UnityEngine;

public class clickDesk : Interactable
{
    public override void InteractStart()
	{
		GameManager.instance.audioManager.TableTap();
	}
}
