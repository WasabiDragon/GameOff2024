using UnityEngine;
using System;
using System.Collections;

public class FocusMode : MonoBehaviour
{
	public GameObject paperFocusPoint;
	public GameObject toolFocusPoint;
	public GameObject focusBackground;
	private Vector3 paperOriginalPos;
	private Vector3 toolOriginalPos;

	private GameObject storedPaper;
	private GameObject storedTool;

	public float transitionSpeed = 2f;
	private bool isEnabled = false;
	private bool isToolEnabled = false;




	public bool FocusEnabled()
	{
		return isEnabled;
	}

	public bool ToolFocusEnabled()
	{
		return isToolEnabled;
	}

	public Tool GetTool()
	{
		if (storedTool == null)
		{
			return null;
		}
		else
		{
			return storedTool.GetComponent<ToolSettings>().tool;
		}
	}

	public Paper FocusedPaper
	{
		get{
			return storedPaper.GetComponent<Paper>();
		}
	}


	public void EnableFocus(GameObject objectToFocus)
	{
		if(objectToFocus.GetComponent<Paper>() != null)
		{
			paperOriginalPos = objectToFocus.transform.position;
			storedPaper = objectToFocus;
			StopAllCoroutines();
			StartCoroutine(MoveToPos(objectToFocus, paperFocusPoint.transform.position));
			focusBackground.SetActive(true);
			isEnabled = true;
		}
		else if (objectToFocus.GetComponent<ToolSettings>() != null)
		{
			toolOriginalPos = objectToFocus.transform.position;
			storedTool = objectToFocus;
			StopAllCoroutines();
			StartCoroutine(MoveToPos(objectToFocus, toolFocusPoint.transform.position));
			isToolEnabled = true;
		}
		else
		{
			Debug.Log("No paper attached to focal object.");
		}
	}

	public void DisablePaperFocus()
	{
		StartCoroutine(MoveToPos(storedPaper, paperOriginalPos));
		focusBackground.SetActive(false);
		isEnabled = false;
	}

	public void DisableToolFocus()
	{
		StartCoroutine(MoveToPos(storedTool, toolOriginalPos));
		isToolEnabled = false;
	}



	private IEnumerator MoveToPos(GameObject objectToMove, Vector3 targetPoint)
	{
		float f = 0;
        while(f < 1f)
        {
            f += Time.deltaTime * transitionSpeed;
            f = Mathf.Clamp(f, 0, 1f);

			Vector3 pos = Vector3.Lerp(objectToMove.transform.position, targetPoint, f);
            objectToMove.transform.position = pos;

            yield return new WaitForEndOfFrame();
        }
	}
}
