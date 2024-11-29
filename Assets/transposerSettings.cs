using System;
using System.Collections.Generic;
using UnityEngine;

public class transposerSettings : ToolSettings
{
	public List<GameObject> dials;
	float segmentSize = 360f/26f;
	Vector3 startingMousePos;
	[SerializeField] Vector3 focussedRotation;
	Vector3 defaultRotation;
	
	void Start()
	{
		Init();
		defaultRotation = transform.localEulerAngles;
	}

	public string CodeWord
	{
		get
		{
			return GetCodeWord();
		}
	}

	void OnMouseDown()
	{
		startingMousePos = Input.mousePosition;
	}

	void OnMouseUp()
	{
		if(Input.mousePosition == startingMousePos)
		{
			Trigger();
		}	
	}

	public void Trigger()
	{
		if(focus.ToolFocusEnabled() && focus.FocusEnabled())
		{
			tool.transposeSetting = GetCodeWord();
			Decode();
		}
		else
		{
			if(focus.ToolFocusEnabled())
			{
				focus.DisableToolFocus();
			}
			else
			{
				focus.EnableFocus(gameObject);
				FocussedRotation(true);
			}
		}
	}

	private string GetCodeWord()
	{
		List<int> numbers = new();
		foreach (GameObject obj in dials)
		{
			numbers.Add(DecodeDial(obj));
		}

		string output = CombineNumbers(GetLetterOrder(numbers));
		return output;
	}

	private List<int> GetLetterOrder(List<int> letterNumbers)
	{
		List<int> sortedNumbers = new List<int>(letterNumbers);
		sortedNumbers.Sort();
		List<int> index = new();
		for (int i = 0; i < sortedNumbers.Count; i++)
		{
			index.Add(letterNumbers.FindIndex(x => x == sortedNumbers[i]));
		}
		return index;
	}

	private string CombineNumbers(List<int> numbers)
	{
		string output = "";
		for (int i = 0; i < numbers.Count; i++)
		{
			output += numbers[i].ToString();
		}


		return output;
	}

	private int DecodeDial(GameObject obj)
	{	
		Vector3 axis = Vector3.forward;
		Vector3 vecB = Quaternion.Euler(new Vector3(0f,0f,0f))*axis;
		Vector3 vecA = obj.transform.rotation*axis;
		float angleA = Mathf.Atan2(vecA.y, vecA.z)*Mathf.Rad2Deg;
		float angleB = Mathf.Atan2(vecB.y, vecB.z)*Mathf.Rad2Deg;
		//returns value between -180 & 180 degrees
		var anglediff = Mathf.DeltaAngle(angleA, angleB);
		if(anglediff <0)
		{
			anglediff += 360f;
		}
		return Mathf.RoundToInt(anglediff/segmentSize)%26;
	}

	public void FocussedRotation(bool v)
	{
		transform.localEulerAngles = v ? focussedRotation : defaultRotation;
	}
}
