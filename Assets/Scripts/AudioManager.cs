using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class AudioManager : MonoBehaviour
{
	
	[SerializeField] EventReference _tableTap;
	[SerializeField] EventReference _backgroundTrack;
	EventInstance _backgroundTrackInst;
	[SerializeField] EventReference _tickSecond;
	[SerializeField] EventReference _tickMinute;
	[SerializeField] EventReference _typewriterTick;
	[SerializeField] EventReference _typewriterDing;
	[SerializeField] EventReference _endOfDay;

	void Start()
	{
		SetSubscriptions();
		_backgroundTrackInst = RuntimeManager.CreateInstance(_backgroundTrack);
	}

	private void SetSubscriptions()
	{
		EventManager.instance.DayEnd += EndDay;
		EventManager.instance.DayEnd += EndMusic;
		EventManager.instance.DayStart += StartMusic;
	}

	public void PaperCrunch()
	{
		RuntimeManager.PlayOneShot(_tableTap);
	}

	public void StartMusic()
	{
		_backgroundTrackInst.start();
	}

	public void EndMusic()
	{
		_backgroundTrackInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}

	/// <summary>
	/// Creates ticking noise for the countdown clock. Variance included.
	/// </summary>
	/// <param name="type">default is small tick. Make true for bigger tick.</param>
	public void ClockTick(bool alternate = false)
	{
		// EventReference _toPlay =  alternate ? _tickMinute : _tickSecond;
		// RuntimeManager.PlayOneShot(_toPlay);
		RuntimeManager.PlayOneShot(_tickSecond);
	} 

	public void TypewriterTick()
	{
		RuntimeManager.PlayOneShot(_typewriterTick);
	}

	public void TypewriterDing()
	{
		RuntimeManager.PlayOneShot(_typewriterDing);
	}

	public void EndDay()
	{
		RuntimeManager.PlayOneShot(_endOfDay);
	}
}
