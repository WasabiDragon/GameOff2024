using TMPro;
using UnityEngine;

public class tinyWriting : MonoBehaviour
{
    [SerializeField] Paper paper;
	[SerializeField] GameObject tinyWritingBox;
	[SerializeField] TextMeshProUGUI tinyText;

	void Start()
	{
		Init();
	}

	void Init()
	{
		if(paper != null && paper.GetInfo().hiddenMessage != null && paper.GetInfo().hiddenMessage.Length > 0)
		{
			tinyWritingBox.SetActive(true);
			WriteMessage(paper.GetInfo().hiddenMessagePos);
		}
	}

	private void WriteMessage(int wordToUnderline)
	{
		paper.paperText.ForceMeshUpdate();
		TMP_TextInfo info = paper.paperText.textInfo;
		Debug.Log("Word Count on "+paper.gameObject.name+": "+info.wordCount);
		Debug.Log(info.wordInfo.Length);
		TMP_WordInfo wordInfo = info.wordInfo[wordToUnderline];

		Vector3 firstCharacterPos = info.characterInfo[wordInfo.firstCharacterIndex].bottomLeft;
		firstCharacterPos = paper.paperText.GetComponent<RectTransform>().TransformPoint(firstCharacterPos);

		Vector3 lastCharacterPos = info.characterInfo[wordInfo.firstCharacterIndex].bottomRight;
		lastCharacterPos = paper.paperText.GetComponent<RectTransform>().TransformPoint(lastCharacterPos);

		RectTransform rtf = tinyWritingBox.GetComponent<RectTransform>();
		rtf.sizeDelta = new Vector2(lastCharacterPos.x - firstCharacterPos.x, rtf.sizeDelta.y);

		firstCharacterPos = paper.gameObject.transform.InverseTransformPoint(firstCharacterPos);
		lastCharacterPos = paper.gameObject.transform.InverseTransformPoint(lastCharacterPos);

		tinyWritingBox.transform.position = new Vector3(rtf.position.x, firstCharacterPos.y, rtf.position.z);
		
		TextMeshProUGUI text = tinyWritingBox.GetComponent<TextMeshProUGUI>();
		text.text = paper.GetInfo().hiddenMessage;
	}

	void OnClick()
	{
		Tool tool = GameManager.instance.focus.GetTool();
		if(tool.type == Tool.toolType.magnifying)
		{
			GameManager.instance.decoder.TranslateMessage(tool);
		}
	}
}
