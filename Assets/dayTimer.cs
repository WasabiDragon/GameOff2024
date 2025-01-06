using System;
using TMPro;
using UnityEngine;

public class dayTimer : MonoBehaviour
{
	[SerializeField] float _defaultTime; // in seconds
	[SerializeField] TextMeshProUGUI _tmpTimer;
	float _dayLength;
	float _timeRemaining;
	bool _timerRunning = false;
	float _updateRate = 1f;
	float _updateCheck = 1f;

	void Start()
	{
		EventManager.instance.DayStart += StartDay;
	}

	public void StartDay()
	{
		_dayLength = _defaultTime;
		StartTimer();
	}

	void StartTimer()
	{
		_timeRemaining = _dayLength;
		UpdateClock();
		_timerRunning = true;
	}

	void Update()
	{
		if(_timerRunning)
		{
			if(_timeRemaining <= 0)
			{
				EventManager.instance.TriggerDayEnd();
				_timerRunning = false;
				return;
			}
			else
			{
				AdjustTimes();
			}
		}
	}

	private void AdjustTimes()
	{
		_updateCheck += Time.deltaTime;
		_timeRemaining -= Time.deltaTime;
		if (_updateCheck >= _updateRate)
		{
			UpdateClock();
			_updateCheck = 0;
		}
	}

	private void UpdateClock()
	{
		int seconds;
		int minutes;
		minutes = Mathf.RoundToInt((int)_timeRemaining / 60);
		seconds = (int)_timeRemaining % 60;
		_tmpTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		Color col = minutes < 1 ? Color.red : Color.white;
		_tmpTimer.color = col;
		
		bool _bigTick = seconds == 0 ? true : false;
		GameManager.instance.audioManager.ClockTick(_bigTick);
	}
}
