using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    [SerializeField] Image paperLogo;
	public TextMeshProUGUI paperText;
	[SerializeField] PaperInfo infoSheet;

	public void SetPaper(PaperInfo paperInfo)
	{
		SetText(paperInfo.paperText);
		SetLogo(paperInfo.paperLogo);
		infoSheet = paperInfo;
	}

	public PaperInfo GetInfo()
	{
		return infoSheet;
	}

	public string GetText()
	{
		return paperText.text;
	}

	public void SetText(string text)
	{
		paperText.text = text;
	}

	public void SetLogo(Sprite image)
	{
		if(image != null)
		{
			paperLogo.sprite = image;
		}
		else
		{
			paperLogo.gameObject.SetActive(false);
		}
	}
}
