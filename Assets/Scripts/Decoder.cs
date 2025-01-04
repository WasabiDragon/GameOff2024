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
			Debug.Log("No message or tool sent to translator.");
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

	public string EncryptMessage(List<PaperInfo.EncryptionStep> steps, string message)
	{
		string output = message;
		if (steps == null || steps.Count <= 0)
		{
			return output;
		}
		foreach(PaperInfo.EncryptionStep step in steps)
		{
			Debug.Log(output);
			switch (step.type)
			{
				case CodeType.cipher:
					output = EncodeCaesar(output, step.offset);
					break;
				case CodeType.transposition:
					output = EncodeTransposer(output, WordsToNumbers(step.keyWord));
					break;
				case CodeType.magnification:
					output = message;
					break;
			}
		}
		Debug.Log("Encrypted Message: "+output);
		return output;
	}

	private string Magnify()
	{
		return focus.FocusedPaper.GetInfo().hiddenMessage;
	}
	
	#region Decryption
	private string DecodeCaesar(string message, int offset)
	{
		bool textTag = false;
		string output = "";
		if(offset < 0)
		{
			offset+=26;
		}
		foreach(char c in message)
		{
			// excluding text tags from the decoding process
			if(c.Equals(char.Parse("<")) || c.Equals(char.Parse(@"\")))
			{
				textTag = !textTag;
			}
			if(textTag)
			{
				output += c;
				if(c.Equals(char.Parse("n")) || c.Equals(char.Parse(">")))
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
		List<string> patternedMessage =GetTextInGroups(separated, pattern);
		foreach(string group in patternedMessage)
		{
			string _groupOutput = "";
			for(int i = 0; i < group.Length; i++)
			{
				for(int j = 0; j < pattern.Length; j++)
				{
					if(char.GetNumericValue(pattern[j]) == i)
					{
						_groupOutput += group[j];
						break;
					}
				}
			}
			Debug.Log("Does "+group+" = "+patternedMessage.Last());
			if(group == patternedMessage.Last())
			{
				Debug.Log("YES!");
				_groupOutput.TrimEnd(char.Parse(" "));
			}
			output += _groupOutput;
		}
		Debug.Log(output);
		return output;
	}
	#endregion Decryption	
	
	#region Encryption
	private string EncodeTransposer(string message, string pattern)
	{
		string output = "";
		separatedMessage separated = RemoveTags(message);
		List<string> patternedMessage = GetTextInGroups(separated, pattern);
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

	private string EncodeCaesar(string message, int offset)
	{
		return DecodeCaesar(message, 0-offset);
	}
	#endregion Encryption

	#region SubClasses
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

	#endregion SubClasses

	#region Util
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

	private List<string> GetTextInGroups(separatedMessage separated, string pattern)
	{
		List<string> patternedMessage = new List<string>();
		for(int i = 0; i < Mathf.CeilToInt((float)(float)separated.untaggedMessage.Length/(float)pattern.ToString().Length); i++)
		{
			int substringLength = i == Mathf.CeilToInt(separated.untaggedMessage.Length/pattern.ToString().Length) ? separated.untaggedMessage.Length%pattern.ToString().Length: pattern.Length;
			string line = separated.untaggedMessage.Substring(i*pattern.Length, substringLength);
			while(line.Length < pattern.Length)
			{
				line += " ";
			}
			patternedMessage.Add(line);
		}
		return patternedMessage;
	}

	private void PrintNewSheet(string message)
	{
		GameManager.instance.paperSpawner.PrintNewPaper(message);
	}

	public string WordsToNumbers(string pattern)
	{	
		List<int> letterNumbers = new List<int>();
		foreach(Char c in pattern)
		{
			letterNumbers.Add((int)c);
		}
		List<int> sortedNumbers = new List<int>(letterNumbers);
		sortedNumbers.Sort();
		List<int> index = new();
		for (int i = 0; i < sortedNumbers.Count; i++)
		{
			index.Add(letterNumbers.FindIndex(x => x == sortedNumbers[i]));
		}
		string output = "";
		foreach(int i in index)
		{
			output += i.ToString();
		}
		return output;
	}
	#endregion Util
}