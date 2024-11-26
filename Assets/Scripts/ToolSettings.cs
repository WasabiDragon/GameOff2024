using UnityEngine;

public class ToolSettings : MonoBehaviour
{
    public Tool tool;
	[ReadOnly] public FocusMode focus;

	void Start()
	{
		Init();
	}

	public void Init()
	{
		focus = GameManager.instance.focus;
	}

	public virtual void Decode()
	{
		GameManager.instance.decoder.TranslateMessage(tool);
	}
}
