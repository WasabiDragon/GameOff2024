using UnityEngine;

public class ToolSettings : MonoBehaviour
{
    public Tool tool;
	[ReadOnly] public FocusMode focus;

	void Start()
	{
		focus = GameManager.instance.focus;
	}

	public virtual void Decode()
	{
		Debug.Log("Tool settings decode not implemented for this object.");
	}
}
