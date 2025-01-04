using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class AudioManager : MonoBehaviour
{
	
	[SerializeField] EventReference _paperCrunch;
	[SerializeField] EventReference _tickSecond;
	[SerializeField] EventReference _tickMinute;
	[SerializeField] EventReference _endOfDay;

	void Start()
	{
		SetSubscriptions();
	}

	private void SetSubscriptions()
	{
		EventManager.instance.DayEnd += EndDay;
	}

	public void PaperCrunch()
	{
		RuntimeManager.PlayOneShot(_paperCrunch);
	}

	/// <summary>
	/// Creates ticking noise for the countdown clock. Variance included.
	/// </summary>
	/// <param name="type">default is small tick. Make true for bigger tick.</param>
	public void ClockTick(bool alternate = false)
	{
		EventReference _toPlay =  alternate ? _tickMinute : _tickSecond;
		RuntimeManager.PlayOneShot(_toPlay);
	}

	public void EndDay()
	{
		RuntimeManager.PlayOneShot(_endOfDay);
	}
}
