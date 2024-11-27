using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTray : Tray
{
	[SerializeField] float openRotation;
	float closedRotation = 0f;
	[SerializeField] GameObject front;
	[SerializeField] float transitionSpeed;
	bool currentlyOpen;

	public void OpenTray()
	{
		Debug.Log("Open folder");
		if (!currentlyOpen)
		{
			currentlyOpen = true;
			StopAllCoroutines();
			StartCoroutine(ChangeTrayState(true));
		}
	}

	public void CloseTray()
	{
		if(currentlyOpen)
		{
			currentlyOpen = false;
			StopAllCoroutines();
			StartCoroutine(ChangeTrayState(false));
		}
	}

	private IEnumerator ChangeTrayState(bool open)
	{
		float f = 0f;
		float y_target = open ? openRotation : closedRotation;
		Vector3 targetRot = front.transform.localEulerAngles;
		targetRot.y = y_target;

		while(f < 1f)
		{
			f += Time.deltaTime * transitionSpeed;
			f = Mathf.Clamp(f,0f,1f);

			Vector3 tRot = Vector3.Lerp(front.transform.localEulerAngles, targetRot, f);
			front.transform.localEulerAngles = tRot;

			yield return new WaitForEndOfFrame();
		}
	}
}