using UnityEngine;

public class followMouse : MonoBehaviour
{
	void Update()
	{
		if(gameObject.activeInHierarchy)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
		}
	}
}
