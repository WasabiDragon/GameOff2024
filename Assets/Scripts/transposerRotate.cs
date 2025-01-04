using UnityEngine;

public class transposerRotate : MonoBehaviour
{
	Vector3 startingMousePos;
	Vector3 startingObjRot;
	public transposerSettings settings;
	public float rotateSpeed = 1;
	bool dragging = false;
	FocusMode focus;
	Quaternion startingRot;
	bool clicked;

	void Start()
	{

		focus = GameManager.instance.focus;
	}

	void OnMouseDown()
	{
		if(GameManager.instance.rays.UI())
		{
			clicked = false;
			return;
		}
		clicked = true;
		startingRot = transform.localRotation;
		startingMousePos = Input.mousePosition;
		startingObjRot = gameObject.transform.localEulerAngles;
	}

	void OnMouseUp()
	{
		dragging = false;
		if(clicked && Input.mousePosition == startingMousePos)
		{
			settings.Trigger();
		}
		clicked = false;
	}

	void OnMouseDrag()
	{
		if(clicked && (dragging || Input.mousePosition != startingMousePos))
		{
			dragging = true;
			float currentDrag = Mathf.Floor(((Input.mousePosition.y-startingMousePos.y)*rotateSpeed)/(360f/26f))*(360f/26f);
			if(currentDrag > 180f || currentDrag < -180f)
			{
				currentDrag += currentDrag > 180f ? -359f : 359f;
			}
			transform.localRotation = startingRot * Quaternion.Euler(currentDrag, 0, 0);
			
		}
	}
}
