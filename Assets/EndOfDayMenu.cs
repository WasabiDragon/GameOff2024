using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayMenu : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _textBox;
	[SerializeField] float _delay = 0.05f;
	[SerializeField] float _lineDelay = 1f;
	[SerializeField] Button _button;
	[SerializeField] string _buttonTextString;
	[SerializeField] TextMeshProUGUI _buttonText;
	string _currentText = "";
	string _targetText = "";
	int _currentDay = 0;

	void Start()
	{
		_textBox.text = "";
		StartCoroutine(Initialize());
	}

	private IEnumerator Initialize()
	{
		yield return StartCoroutine(TypeText(GenerateText(true), _textBox));
		
		_buttonText.text = "";
		_button.gameObject.SetActive(true);
		StartCoroutine(TypeText(_buttonTextString, _buttonText));
	}

	private IEnumerator EndDaySequence(bool addButton)
	{
		yield return StartCoroutine(TypeText(GenerateText(), _textBox));
		_button.gameObject.SetActive(addButton);
		if(addButton)
		{
			_buttonText.text = "";
			StartCoroutine(TypeText(_buttonTextString, _buttonText));
		}
	}

	public void EndDay(int day)
	{
		_currentDay = day;
		_currentText = "";
		_textBox.text = "";
		StartCoroutine(EndDaySequence(GameManager.instance.dailyWork.papers.Count >= _currentDay+1));
	}

	private string GenerateText(bool newGame = false)
	{
		_targetText = "";
		if(newGame)
		{
			_targetText = "Welcome.\nThank you for joining us.\nAll required information is displayed on the papers.\nYour day will now commence.";
			return _targetText;
		}

		ScoreCalc.DailyScore _score = GameManager.instance.score.GetCurrentScore();
		int _scorePercent = _score.TotalSubmitted == 0 ? 0 : (_score.TotalScore/_score.TotalSubmitted)*100;
		_targetText += "Day "+_currentDay.ToString()+" concluded.\n";

		if(GameManager.instance.dailyWork.papers.Count < _currentDay+1)
		{
			_targetText = "Your participation in the C.R.U.D.E training course is appreciated.\nYour final score is: "+_score.TotalScore+" out of "+_score.TotalSubmitted+".";
		}
		else if(_score.DailyResults != null)
		{
			_targetText += "Results so far are as follows:\n";
			foreach(string text in _score.DailyResults)
			{
				_targetText += text + "\n";
			}
			_targetText += "\nYour current success rate is "+_scorePercent.ToString()+" percent.";
		}
		else
		{
			_targetText += "You have failed to submit any files today.";
			_targetText += "\nYour current success rate is "+_scorePercent.ToString()+" percent.";
		}

		return _targetText;
	}

	public void StartDay()
	{
		_buttonText.text = "";
		_button.gameObject.SetActive(false);
		EventManager.instance.TriggerDayStart();
	}

	IEnumerator TypeText(string text, TextMeshProUGUI textLocation)
	{
		for(int i = 0; i < text.Length; i++)
		{
			_currentText = text.Substring(0,i+1);
			if(text[i] == char.Parse(@"\"))
			{
				yield return new WaitForSeconds(_lineDelay);
				continue;
			}
			textLocation.text = _currentText;
			yield return new WaitForSeconds(_delay);
		}
	}
}
