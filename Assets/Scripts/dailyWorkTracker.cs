using UnityEngine;

public class dailyWorkTracker : MonoBehaviour
{
    public DailyPapers papers;
	void Start()
	{
		GameManager.instance.paperSpawner.SpawnPapersInTray(papers);
	}
}
