using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteract : Interactable
{
	bool stopInteract = false;
	bool dragging = false;
	[SerializeField] float dragHeight = 1f;
	float distanceToScreen = 0;
	Vector3 mouseStartPoint;
	FocusMode focus;
	GameObject currentTray;
	SnapPoints snap;
	List<GameObject> draggedObjects;
	Paper thisPaper;
	// bool clicked;

	void Start()
	{
		ParentedToOther = false;
		thisPaper = GetComponent<Paper>();
		focus = GameManager.instance.focus;
		snap = GameManager.instance.snapping;
		if(focus == null)
		{
			Debug.Log("No focus mode found");
		}
		draggedObjects = new List<GameObject>();
	}


	public override void InteractStart()
	{
		distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		mouseStartPoint = Input.mousePosition;
		stopInteract = focus.FocusEnabled();
	}
	
	public override void InteractEnd()
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
			// clicked = false;
		}
	}

	public override void InteractDrag()
	{
		if(stopInteract || ParentedToOther)
		{
			return;
		}
		if(dragging || Input.mousePosition != mouseStartPoint)
		{
			dragging = true;
			Transform snapCheck = snap.Check(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen - dragHeight)));
			if(snapCheck == null)
			{
				// currentTray?.GetComponent<OutTray>()?.CloseTray();
				currentTray = null;
				FollowMouse();
			}
			else
			{
				if(currentTray != snap.currentSnap)
				{
					currentTray = snap.currentSnap;
					// currentTray?.GetComponent<OutTray>()?.OpenTray();
				}
				SnapTo(snapCheck);
			}
		}
	}

	public bool ParentedToOther {get;set;}

	private void SnapTo(Transform tf = null)
	{
		if (tf != null)
		{
			Vector3 pos = new Vector3(tf.position.x, tf.position.y, distanceToScreen - dragHeight);
			TransformPapers(thisPaper.isAttached ? thisPaper.attachedTo : gameObject, pos, tf.rotation.eulerAngles.z);
		}
	}

	private void DropObject()
	{
		StopAllCoroutines();
		Vector3 placeObj = transform.position;
		placeObj.z += dragHeight;
		GameObject targetPaper = thisPaper.isAttached ? thisPaper.attachedTo : gameObject;
		TransformPapers(targetPaper,placeObj);

		GameManager.instance.paperSorter.UpdateTopSheet(gameObject);
		if(currentTray != null && currentTray.GetComponent<Tray>() != null)
		{
			currentTray.GetComponent<Tray>().AddToTray(thisPaper);
			// currentTray?.GetComponent<OutTray>()?.CloseTray();
			currentTray = null;
		}
	}

	private void FollowMouse()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen - dragHeight));
		GameObject targetPaper = thisPaper.isAttached ? thisPaper.attachedTo : gameObject;
		TransformPapers(targetPaper, pos,0,false,true);
	}

	private void TransformPapers(GameObject mainPaper, Vector3 targetPos, float specifiedStartRot = 0, bool syncRot = false, bool setRotZero = false)
	{
		mainPaper.transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, specifiedStartRot > 0 ? specifiedStartRot : setRotZero ? 0 : mainPaper.transform.eulerAngles.z);
		PaperInfo original = mainPaper.GetComponent<Paper>().GetInfo();
		float rotOffset = 5f;
		mainPaper.transform.position = targetPos;
		if(original.attachedPapers == null)
		{
			return;
		}
		else
		{
			foreach(GameObject paper in original.attachedPapers)
			{
				Vector3 targetRot = mainPaper.transform.eulerAngles;
				targetRot.z = specifiedStartRot > 0 ? specifiedStartRot+rotOffset : targetRot.z + rotOffset;
				paper.transform.eulerAngles = targetRot;
				if(!syncRot)
				{
					rotOffset +=5f;
				}
				Vector3 targetOffset = targetPos;
				targetOffset.z = mainPaper.transform.position.z + rotOffset/1000f;
				paper.transform.position = targetOffset;
			}
		}
	}
}
