using UnityEngine;

public class cameraRotation : MonoBehaviour
{
	Camera mainCam;

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
		Vector3 point = mainCam.gameObject.transform.position - transform.position;
		point = -point;
		point = point.normalized;
		transform.rotation = Quaternion.LookRotation(point);
	}
}
