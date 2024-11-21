using UnityEngine;

[CreateAssetMenu(menuName ="Data/Paper Info")]
public class PaperInfo : ScriptableObject
{
	public PaperInfo(string message)
	{	
		paperText = message;
	}
	public Sprite paperLogo;
	[Multiline]public string paperText;
	public bool important;
	public CodeType type;
	[Multiline] public string hiddenMessage;

	[Tooltip("X = line, Y = word")]
	public int hiddenMessagePos;
}
