using UnityEngine;

public class ExitFocus : MonoBehaviour
{
	void OnMouseUp()
	{
		GameManager.instance.focus.DisablePaperFocus();	
	}
}
