using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Paper Info")]
public class PaperInfo : ScriptableObject
{
	public static PaperInfo CreateInstance(PaperInfo info)
	{
		var data = ScriptableObject.CreateInstance<PaperInfo>();
		data.Init(info);
		return data;
	}

	private void Init(PaperInfo info)
	{
		paperLogo = info.paperLogo;
		printed = info.printed;
		displayText = info.displayText;
		originalText = info.originalText;
		important = info.important;
		originalPaper = info.originalPaper;
		encryptionSteps = info.encryptionSteps;
		hiddenMessage = info.hiddenMessage;
		hiddenMessagePos = info.hiddenMessagePos;
		boundTo = null;
		attachedPapers = new List<GameObject>();
		name = info.name+"_inst";
	}
	public Sprite paperLogo;
	[ReadOnly] public bool printed = false;
	[ReadOnly] public string displayText;
	[TextArea(5,20)][Multiline]public string originalText;
	public bool important;
	public bool originalPaper = false;
	public List<EncryptionStep> encryptionSteps;
	[Multiline] public string hiddenMessage;
	public GameObject boundTo;

	[Tooltip("X = line, Y = word")]
	public int hiddenMessagePos;

	public List<GameObject> attachedPapers;

	[System.Serializable]
	public class EncryptionStep
	{
		public CodeType type;
		public int offset;
		public string keyWord;
	}
}
