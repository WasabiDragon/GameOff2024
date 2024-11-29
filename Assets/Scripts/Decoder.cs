using System;
using System.Collections.Generic;
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
			case Tool.toolType.transposer:
				outputMessage = DecodeTransposer(message, decodeTool.transposeSetting);
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

	private string DecodeTransposer(string message, string pattern)
	{
		string output = "";
		separatedMessage separated = RemoveTags(message);
		List<string> patternedMessage = new List<string>();
		for(int i = 0; i < separated.untaggedMessage.Length/pattern.ToString().Length; i++)
		{
			patternedMessage.Add(separated.untaggedMessage.Substring(i*pattern.Length, pattern.Length));
		}

		foreach(string group in patternedMessage)
		{
			for(int i = 0; i < group.Length; i++)
			{
				int index = (int)Char.GetNumericValue(pattern[i]);
				if(index >= group.Length)
				{
					continue;
				}
				output += group[index].ToString();
			}
		}

		return ReintroduceTags(new separatedMessage(separated.tags, output));
	}
	
	[System.Serializable]
	private class separatedMessage
	{
		public separatedMessage(Dictionary<int, string> tagDict, string taglessMessage)
		{
			tags = tagDict;
			untaggedMessage = taglessMessage;
		}

		public Dictionary<int, string> tags;
		public string untaggedMessage;
	}

	private separatedMessage RemoveTags(string message)
	{
		bool textTag = false;
		Dictionary<int, string> output = new();
		string untaggedMessage = "";
		string holdingString = "";
		int insertPos = 0;
		int index = 0;
		foreach(char c in message)
		{
			index += 1;
			if(c.Equals(char.Parse("<")) || c.Equals(char.Parse(@"\")))
			{
				textTag = true;
				insertPos = index;
			}
			if(textTag)
			{
				holdingString += c;

				if(c.Equals(char.Parse("n")) || c.Equals(char.Parse(">")))
				{
					output.Add(insertPos, holdingString);
					holdingString = "";
					textTag = false;
				}
				continue;
			}
			else
			{
				untaggedMessage += c;
			}
		}
		return new separatedMessage(output, untaggedMessage);
	}

	private string ReintroduceTags(separatedMessage message)
	{
		string output = message.untaggedMessage;
		foreach(KeyValuePair<int, string> pair in message.tags)
		{
			output.Insert(pair.Key, pair.Value.ToString());
		}
		return output;
	}
}