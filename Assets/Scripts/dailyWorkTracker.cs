using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dailyWorkTracker : MonoBehaviour
{
	[SerializeField] GameObject _endOfDayScreen;
	[SerializeField] GameObject _endOfDayText;
	[SerializeField] float _transitionSpeed;
    public List<DailyPapers> papers = new List<DailyPapers>();
	int _currentDay = 0;



	void Start()
	{
		EventManager.instance.DayEnd += EndDay;
		EventManager.instance.DayStart += StartDay;
		// StartDay();
	}

	public void StartDay()
	{
		if(_endOfDayScreen.activeInHierarchy)
		{
			StartCoroutine(FadeObject(false));
			StartCoroutine(FadeObject(false, _endOfDayText));
		}
		_currentDay +=1;
		if(papers == null || papers.Count < _currentDay)
		{
			Debug.LogError("Not enough Daily Papers to start day");
		}
		DailyPapers _spawnList = papers[_currentDay-1];
		GameManager.instance.paperSpawner.SpawnPapersInTray(_spawnList);
	}

	public void EndDay()
	{
		//disable focus mode
		GameManager.instance.focus.DisablePaperFocus();
		GameManager.instance.focus.DisableToolFocus();

		//block clicks with UI
		_endOfDayScreen.SetActive(true);
		StartCoroutine(FadeObject(true));
		StartCoroutine(FadeObject(true,_endOfDayText));

		//display EndOfDayResults
		_endOfDayScreen.GetComponent<EndOfDayMenu>().EndDay(_currentDay);


		//cleanFolders
		//StartNextDay if available
	}

	/// <summary>
	/// Activate the End of Day panels & UI.
	/// </summary>
	/// <param name="show">True to display, false to hide.</param>
	/// <returns></returns>
	private IEnumerator FadeObject(bool show, GameObject obj = null)
	{
		if(obj == null)
		{
			obj = _endOfDayScreen;
		}
		Image _img = obj.GetComponent<Image>();
		TextMeshProUGUI _txtCol = obj.GetComponent<TextMeshProUGUI>();

		bool _changeImage = _img != null;

		Color _targetColor = _changeImage ? _img.color : _txtCol.color;
		_targetColor.a = show ? 1 : 0;
		float f = 0;
        while(f < 1f)
        {
            f += Time.deltaTime * _transitionSpeed;
            f = Mathf.Clamp(f, 0, 1f);

			Color _col = Color.Lerp(_changeImage ? _img.color : _txtCol.color, _targetColor, f);
            if(_changeImage)
			{
				_img.color = _col;
			}
			else
			{
				_txtCol.color = _col;
			}
            yield return new WaitForEndOfFrame();
        }
		_endOfDayScreen.SetActive(show);
	}
}
