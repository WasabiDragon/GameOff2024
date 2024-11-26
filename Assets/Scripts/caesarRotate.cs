using UnityEngine;

public class caesarRotate : MonoBehaviour
{
	Vector3 startingMousePos;
	Vector3 startingObjRot;
	public CaesarSettings settings;
	public float rotateSpeed = 1;
	bool dragging = false;
	FocusMode focus;

	void Start()
	{
		focus = GameManager.instance.focus;
	}

	void OnMouseDown()
	{
		startingMousePos = Input.mousePosition;
		startingObjRot = gameObject.transform.rotation.eulerAngles;
	}

	void OnMouseUp()
	{
		dragging = false;
		if(Input.mousePosition == startingMousePos)
		{
			settings.Trigger();
		}	
	}

	void OnMouseDrag()
	{
		if(dragging || Input.mousePosition != startingMousePos)
		{
			dragging = true;
			float snappedPos = Mathf.Floor((startingMousePos.x - Input.mousePosition.x)*rotateSpeed)*(360f/26f);
			gameObject.transform.eulerAngles = new Vector3(startingObjRot.x,startingObjRot.y, snappedPos);
		}
	}
}
