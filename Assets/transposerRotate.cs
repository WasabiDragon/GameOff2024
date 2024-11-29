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

	void Start()
	{

		focus = GameManager.instance.focus;
	}

	void OnMouseDown()
	{
		startingRot = transform.localRotation;
		startingMousePos = Input.mousePosition;
		startingObjRot = gameObject.transform.localEulerAngles;
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
			float currentDrag = Mathf.Floor(((Input.mousePosition.y-startingMousePos.y)*rotateSpeed)/(360f/26f))*(360f/26f);
			if(currentDrag > 180f || currentDrag < -180f)
			{
				bool tooVast = true;
				while(tooVast)
				{
					currentDrag =+ currentDrag > 180f ? -360f : 360f;
					if(currentDrag <= 180f && currentDrag >= -180f)
					{
						tooVast = false;
					}
				}
			}
			transform.localRotation = startingRot * Quaternion.Euler(currentDrag, 0, 0);
			
		}
	}
}
