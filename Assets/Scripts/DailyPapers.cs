using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Daily Papers")]
public class DailyPapers : ScriptableObject
{
	public List<PaperInfo> dailyPapers;
}
