using UnityEngine;
using System;
using System.Collections;

public class FocusMode : MonoBehaviour
{
	public GameObject paperFocusPoint;
	public GameObject toolFocusPoint;
	public GameObject transposerFocusPoint;
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
			return storedPaper?.GetComponent<Paper>();
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
			GameObject focalPoint = storedTool.GetComponent<ToolSettings>().tool.type == Tool.toolType.transposer ? transposerFocusPoint : toolFocusPoint;
			StartCoroutine(MoveToPos(objectToFocus, focalPoint.transform.position));
			isToolEnabled = true;
		}
		else
		{
			Debug.Log("No paper attached to focal object.");
		}
	}

	public void DisablePaperFocus()
	{
		if(storedPaper == null)
		{
			return;
		}
		StartCoroutine(MoveToPos(storedPaper, paperOriginalPos));
		focusBackground.SetActive(false);
		isEnabled = false;
		storedPaper = null;
	}

	public void DisableToolFocus()
	{
		if(storedTool == null)
		{
			return;
		}
		StartCoroutine(MoveToPos(storedTool, toolOriginalPos));
		storedTool?.GetComponent<transposerSettings>()?.FocussedRotation(false);
		isToolEnabled = false;
		storedTool = null;
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
