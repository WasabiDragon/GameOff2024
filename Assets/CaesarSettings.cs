using UnityEngine;

public class CaesarSettings : ToolSettings
{
	public GameObject innerWheel;
	float segmentSize = 360f/26f;

	[ReadOnly]public int Offset
	{
		get{
			float rotation = innerWheel.transform.localRotation.eulerAngles.z;
			return Mathf.RoundToInt(rotation/segmentSize);
		}
	}

	public override void Decode()
	{
		tool.toolSetting = Offset;
		GameManager.instance.decoder.TranslateMessage(tool);
	}

	public void Trigger()
	{
		if(focus.ToolFocusEnabled() && focus.FocusEnabled())
		{
			Decode();
		}
		else
		{
			if(focus.ToolFocusEnabled())
			{
				focus.DisableToolFocus();
			}
			else
			{
				focus.EnableFocus(gameObject);
			}
		}
	}
}