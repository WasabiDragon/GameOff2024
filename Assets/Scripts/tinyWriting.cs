using TMPro;
using UnityEngine;

public class tinyWriting : MonoBehaviour
{
    [SerializeField] Paper paper;
	[SerializeField] GameObject tinyWritingBox;
	[SerializeField] TextMeshProUGUI tinyText;
	[SerializeField] Canvas canvas;
	FocusMode focus;

	void Start()
	{
		Init();
	}

	void Init()
	{
		focus = GameManager.instance.focus;
		if(paper != null && paper.GetInfo().smallTextDisplay != null && paper.GetInfo().smallTextDisplay.Length > 0)
		{
			tinyWritingBox.SetActive(true);
			WriteMessage(paper.GetInfo().hiddenMessagePos);
		}
	}

	private void WriteMessage(int wordToUnderline)
	{
		paper.paperText.ForceMeshUpdate();
		TMP_TextInfo info = paper.paperText.textInfo;
		TMP_WordInfo wordInfo = info.wordInfo[wordToUnderline];
		
		info.characterInfo[wordInfo.firstCharacterIndex].color = new(255,0,0,255);
		
		Vector3 firstCharacterPos = info.characterInfo[wordInfo.firstCharacterIndex].bottomLeft;
		Vector3 lastCharacterPos = info.characterInfo[wordInfo.lastCharacterIndex].bottomRight;

		RectTransform rtf = tinyWritingBox.GetComponent<RectTransform>();
		rtf.sizeDelta = new Vector2(lastCharacterPos.x - firstCharacterPos.x, rtf.sizeDelta.y);

		tinyWritingBox.transform.localPosition = new Vector3((lastCharacterPos.x + firstCharacterPos.x)/2, firstCharacterPos.y, 0f);
		
		tinyText.text = paper.GetInfo().smallTextDisplay;
	}

	void OnMouseDown()
	{
		if(focus.FocusedPaper == paper && focus.GetTool() != null && focus.GetTool().type == Tool.toolType.magnifying)
		{
			GameManager.instance.magnifyingSettings.Trigger();
		}
	}
	
}
