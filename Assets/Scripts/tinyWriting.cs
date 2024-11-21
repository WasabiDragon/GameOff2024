using TMPro;
using UnityEngine;

public class tinyWriting : MonoBehaviour
{
    [SerializeField] Paper paper;
	[SerializeField] GameObject tinyWritingBox;
	[SerializeField] TextMeshProUGUI tinyText;
	FocusMode focus;

	void Start()
	{
		Init();
	}

	void Init()
	{
		focus = GameManager.instance.focus;
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
		
		tinyText.text = paper.GetInfo().hiddenMessage;
	}

	void OnClick()
	{
		if(focus.FocusedPaper == paper && focus.GetTool().type == Tool.toolType.magnifying)
		{
			GameManager.instance.magnifyingSettings.Trigger();
		}
	}
	
}
