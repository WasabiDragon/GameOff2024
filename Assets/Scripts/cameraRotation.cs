using UnityEngine;

public class cameraRotation : MonoBehaviour
{
	Camera mainCam;
	[SerializeField] GameObject targetObject;

	void Start()
	{
		mainCam = Camera.main;
	}

	void Update()
	{
		if(enabled)
		{
			RotateCam();
		}
	}

	void RotateCam()
	{
		Vector3 point = mainCam.gameObject.transform.position - mainCam.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, targetObject.transform.position.z));
		point = -point;
		point = point.normalized;
		transform.rotation = Quaternion.LookRotation(point);
	}
}
