using System;
using UnityEngine;

public class magnifierSettings : ToolSettings
{
	Bounds tinyBounds;
	[SerializeField] GameObject magnifier;

	void Start()
	{
		InitTool();
	}

	public void Trigger()
	{
		Decode();
	}

	public void OnMouseUp()
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

	void Update()
	{
		if(focus.FocusedPaper != null && focus.GetTool() != null && focus.GetTool().type == Tool.toolType.magnifying)
		{
			magnifier.SetActive(true);
		}
		else if(magnifier.activeInHierarchy)
		{
			magnifier.SetActive(false);
		}
	}
}
