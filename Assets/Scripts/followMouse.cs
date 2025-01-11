using UnityEngine;
using UnityEngine.InputSystem;

public class followMouse : MonoBehaviour
{
	[ReadOnly] public Vector3 mousePosTest;

	void Update()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
		transform.position = mousePos;
		mousePosTest = mousePos;
	}
}
