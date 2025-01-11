using UnityEngine;

public class ToolSettings : MonoBehaviour
{
    public Tool tool;
	[ReadOnly] public FocusMode focus;

	public void InitTool()
	{
		focus = GameManager.instance.focus;
	}

	public virtual void Decode()
	{
		GameManager.instance.decoder.TranslateMessage(tool);
	}
}
