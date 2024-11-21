using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaperInteract : MonoBehaviour
{
	bool stopInteract = false;
	bool dragging = false;
	[SerializeField] float dragHeight = 1f;
	float distanceToScreen = 0;
	Vector3 mouseStartPoint;
	FocusMode focus;


	void Start()
	{
		focus = GameManager.instance.focus;
		if(focus == null)
		{
			Debug.Log("No focus mode found");
		}
	}


	void OnMouseDown()
	{
		distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		mouseStartPoint = Input.mousePosition;
		stopInteract = focus.FocusEnabled();
	}
	
	void OnMouseUp()
	{
		if(stopInteract)
		{
			return;
		}
		if(dragging)
		{
			dragging = false;
			DropObject();
		}
		else
		{
			focus.EnableFocus(gameObject);
		}
	}

	void OnMouseDrag()
	{
		if(stopInteract)
		{
			return;
		}
		if(dragging || Input.mousePosition != mouseStartPoint)
		{
			dragging = true;
			Transform snapCheck = GameManager.instance.snapping.Check(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen - dragHeight)));
			if(snapCheck == null)
			{
				transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
				FollowMouse();
			}
			else
			{
				transform.position = new Vector3(snapCheck.position.x, snapCheck.position.y, transform.position.z);
				transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, snapCheck.rotation.eulerAngles.z);
			}
		}
	}

	private void DropObject()
	{
		StopAllCoroutines();
		Vector3 placeObj = transform.position;
		placeObj.z += dragHeight;
		transform.position = placeObj;

		GameManager.instance.paperSorter.SortList(gameObject);
	}

	private void FollowMouse()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen - dragHeight));
		transform.position = pos;
	}
}
