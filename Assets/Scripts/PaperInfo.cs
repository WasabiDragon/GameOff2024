using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Paper Info")]
public class PaperInfo : ScriptableObject
{
	public Sprite paperLogo;
	[ReadOnly] public bool printed = false;
	[Multiline]public string paperText;
	public bool important;
	public CodeType type;
	[Multiline] public string hiddenMessage;

	[Tooltip("X = line, Y = word")]
	public int hiddenMessagePos;

	public List<PaperInfo> attachedPapers;

	public void AttachPaper(PaperInfo info)
	{
		if(attachedPapers == null)
		{
			attachedPapers = new List<PaperInfo>();
		}
		if(attachedPapers.Contains(info))
		{
			return;
		}
		else
		{
			attachedPapers.Add(info);
		}
	}

	public void DetachAll()
	{
		attachedPapers.Clear();
	}
}
