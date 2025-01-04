using System.Collections.Generic;
using UnityEngine;

public class ScoreCalc : MonoBehaviour
{
	int _totalScore = 0;
	int _totalSubmitted = 0;
	[SerializeField] Tray _tray;

	public struct DailyScore
	{
		public List<string> DailyResults;
		public int TotalScore;
		public int TotalSubmitted;

		public DailyScore(List<string> results, int score, int total)
		{
			DailyResults = results;
			TotalScore = score;
			TotalSubmitted = total;
		}
	}
	
	

	List<string> GetDailyResult()
	{
		List<Paper> _paperStacks = _tray.objectsInTray;
		List<string> _result = new List<string>();

		if( _paperStacks.Count <= 0 )
		{
			Debug.Log("No papers stored in out tray.");
			return null;
		}
		foreach(Paper paper in _paperStacks)
		{
			bool solved = false;
			PaperInfo info = paper.GetInfo();
			if(info.important)
			{
				Debug.Log("Scoring "+paper.gameObject.name);
				_totalSubmitted += 1;
				if(info.attachedPapers != null && info.attachedPapers.Count > 0)
				{
					foreach(GameObject sheet in info.attachedPapers)
					{
						if(solved || sheet.GetComponent<Paper>().GetInfo().displayText == info.originalText)
						{
							Debug.Log(paper.gameObject.name+" is solved.");
							solved = true;
							_totalScore += 1;
						}
					}
				}
				string paperOutcome = solved ? "Success" : "Failure";
				_result.Add("Paper Number "+paper.gameObject.name.Substring(0,3)+": "+paperOutcome+".");
			}
		}
		return _result;
	}

	public DailyScore GetCurrentScore()
	{
		return new DailyScore(GetDailyResult(), _totalScore, _totalSubmitted);
	}
}
