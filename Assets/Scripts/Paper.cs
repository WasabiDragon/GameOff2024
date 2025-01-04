using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    [SerializeField] Image paperLogo;
	public TextMeshProUGUI paperText;
	[SerializeField] PaperInfo infoSheet;
	[SerializeField] tinyWriting tinyWriting;

	public void SetPaper(PaperInfo paperInfo)
	{
		SetText(paperInfo.displayText);
		SetLogo(paperInfo.paperLogo);
		infoSheet = PaperInfo.CreateInstance(paperInfo);
	}

	public PaperInfo GetInfo()
	{
		return infoSheet;
	}

	public void AttachPaper(GameObject obj)
	{
		if(infoSheet.attachedPapers == null)
		{
			infoSheet.attachedPapers = new List<GameObject>();
		}
		if(infoSheet.attachedPapers.Contains(obj))
		{
			return;
		}
		else
		{
			infoSheet.attachedPapers.Add(obj);
			obj.GetComponent<Paper>().AttachToPaper(gameObject);
		}
	}

	public void AttachToPaper(GameObject obj)
	{
		infoSheet.boundTo = obj;
	}

	public void DetachAll()
	{
		foreach(GameObject obj in infoSheet.attachedPapers)
		{
			GameManager.instance.paperSorter.AddPaper(obj);
			obj.GetComponent<Paper>().DetachFromOthers();
			obj.GetComponent<PaperInteract>().ParentedToOther = false;
		}
		infoSheet.attachedPapers.Clear();
	}

	public void DetachFromOthers()
	{
		infoSheet.boundTo = null;
	}

	public bool isAttached
	{
		get{
			return infoSheet.boundTo != null;
		}
	}

	public GameObject attachedTo
	{
		get
		{
			return infoSheet.boundTo;
		}
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
