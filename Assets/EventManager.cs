using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

	void Awake()
	{
		instance = this;
	}

	public Action DayEnd;
	public Action DayStart;

	public void TriggerDayEnd()
	{
		DayEnd.Invoke();
	}

	public void TriggerDayStart()
	{
		DayStart.Invoke();
	}
}
