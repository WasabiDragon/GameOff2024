using UnityEngine;

public class clickDesk : MonoBehaviour
{
    void OnMouseDown()
	{
		GameManager.instance.audioManager.PaperCrunch();
	}
}
