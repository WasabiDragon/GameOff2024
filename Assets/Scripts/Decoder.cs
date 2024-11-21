using System;
using System.Linq;
using UnityEngine;

public class Decoder : MonoBehaviour
{
	FocusMode focus;

	void Start()
	{
		focus = GameManager.instance.focus;	
	}

    public void TranslateMessage(Tool decodeTool)
	{
		string outputMessage = "";
		string message = focus.FocusedPaper.GetText();
		if(message == null || message.Length <= 0 || decodeTool == null)
		{
			Debug.Log("No message or tool sent to decoder.");
			return;
		}
		switch (decodeTool.type)
		{
			case Tool.toolType.magnifying:
				outputMessage = Magnify();
				PrintNewSheet(outputMessage);
				return;
			case Tool.toolType.ceasar:
				outputMessage = DecodeCaesar(message, decodeTool.toolSetting);
				PrintNewSheet(outputMessage);
				return;
		}

		return;
	}


	private void PrintNewSheet(string message)
	{
		GameManager.instance.paperSpawner.PrintNewPaper(message);
	}

	private string Magnify()
	{
		return focus.FocusedPaper.GetInfo().hiddenMessage;
	}
	
	private string DecodeCaesar(string message, int offset)
	{
		bool textTag = false;
		string output = "";
		foreach(char c in message)
		{
			// excluding text tags from the decoding process
			if(c.Equals("<") || c.Equals(@"\"))
			{
				textTag = !textTag;
			}
			if(textTag)
			{
				output += c;
				if(c.Equals("n") || c.Equals(">"))
				{
					textTag = !textTag;
				}
				continue;
			}
			else
			{
				if(char.IsLetter(c))
				{
					bool isUpper = false;
					if( c < char.Parse("a"))
					{
						isUpper = true;
					}

					char outputLetter = char.ToLower(c);
					int index = outputLetter + offset;
					if(index > char.Parse("z"))
					{
						index -= 26;
					}

					outputLetter = isUpper ? char.ToUpper((char)index) : (char)index;
					output += outputLetter;
				}
				else
				{
					output += c;
				}
			}
		}
		return output;
	}
}
