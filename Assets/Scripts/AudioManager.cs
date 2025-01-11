using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	
	[SerializeField] EventReference _tableTap;
	EventInstance _tableTapInst;
	[SerializeField] EventReference _backgroundTrack;
	EventInstance _backgroundTrackInst;
	[SerializeField] EventReference _tickSecond;
	[SerializeField] EventReference _tickMinute;
	[SerializeField] EventReference _typewriterTick;
	[SerializeField] EventReference _typewriterDing;
	[SerializeField] EventReference _endOfDay;

	private void OneShotPitch(EventReference soundRef, float pitch = 0f)
	{
		try
        {
            var instance = RuntimeManager.CreateInstance(soundRef);
			if(pitch == 0f)
			{
				pitch = 1f;
			}
			instance.setPitch(pitch);
			instance.start();
			instance.release();
        }
        catch (EventNotFoundException)
        {
            Debug.LogWarning("[FMOD] Event not found: " + soundRef);
        }
	}

	public bool ready
	{
		get
		{
			return RuntimeManager.HasBankLoaded("Master");
		}
		
	}
	void Start()
	{
		SetSubscriptions();
		_backgroundTrackInst = RuntimeManager.CreateInstance(_backgroundTrack);
		_tableTapInst = RuntimeManager.CreateInstance(_tableTap);
	}

	private void SetSubscriptions()
	{
		EventManager.instance.DayEnd += EndDay;
		EventManager.instance.DayEnd += EndMusic;
		EventManager.instance.DayStart += StartMusic;
	}

	public void TableTap()
	{
		float pitch = UnityEngine.Random.Range(0.85f,1.35f);
		OneShotPitch(_tableTap, pitch);
	}

	public void StartMusic()
	{
		StartCoroutine(StartMusicDelayed());
	}

	private IEnumerator StartMusicDelayed()
	{
		yield return new WaitUntil(() => RuntimeManager.HasBankLoaded("Master") == true);
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
		float pitch = UnityEngine.Random.Range(0.85f,1.35f);
		OneShotPitch(_tickSecond, pitch);
	} 

	public void TypewriterTick()
	{
		float pitch = UnityEngine.Random.Range(0.85f,1.35f);
		OneShotPitch(_typewriterTick);
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
