using UnityEngine;
using System;

[CreateAssetMenu(menuName ="Data/Tool")]
public class Tool : ScriptableObject
{
	public Tool(toolType type, string name, string about, int quant)
	{
		this.type = type;
		toolName = name;
		description = about;
		toolSetting = quant;

	}
	public enum toolType
	{
		magnifying,
		ceasar
	}
	public toolType type;
	public string toolName;
	public string description;
	public int toolSetting;
}