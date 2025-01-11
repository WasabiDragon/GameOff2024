using UnityEngine;
using Unity.Cinemachine;

public class CameraScaler : MonoBehaviour
{
	[SerializeField] float targetWidth;
	[SerializeField] float targetHeight;
	float previousScale;
	float targetScale;
	Camera cam;

	void Start()
	{
		Init();
	}

	private void Init()
	{
		targetScale = targetWidth / targetHeight;
		cam = Camera.main;
		previousScale = GetCurrentScale();
		if(previousScale != targetScale)
		{
			ScaleScreen();
		}
	}

	void Update()
	{
		float aspectCheck = GetCurrentScale();
		if(aspectCheck != previousScale)
		{
			ScaleScreen();
			previousScale = aspectCheck;
		}
	}

	float GetCurrentScale()
	{
		float currentScale;
		currentScale = (float)Screen.width/ (float)Screen.height;
		return currentScale;
	}

	void ScaleScreen()
	{
		float windowAspect = GetCurrentScale();

		float scaleHeight = windowAspect / targetScale;

		if(scaleHeight < 1f)
		{
			Rect rect = cam.rect;
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1f-scaleHeight)/2f;

			cam.rect = rect;
		}
		else
		{
			float scalewidth = 1f/scaleHeight;

			Rect rect = cam.rect;
			rect.width = scalewidth;
			rect.height = 1f;
			rect.x = (1f-scalewidth)/2f;
			rect.y = 0;

			cam.rect = rect;
		}
	}
}
