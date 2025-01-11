using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class paperSorter : MonoBehaviour
{
	[ReadOnly] public List<GameObject> papers = new List<GameObject>();
	private GameObject tempSortCheck;
	private float startingHeight;

	public float AddPaper(GameObject paper)
	{
		papers.Add(paper);
		return startingHeight - (papers.Count / 100f);
	}

	public void RemovePaper(GameObject paper)
	{
		if(papers == null || papers.Count <= 0 || !papers.Contains(paper))
		{
			return;
		}
		papers.Remove(paper);
	}
	
	public void SetStartingHeight(float pos)
	{
		startingHeight = pos;
	}
	
	public void UpdateTopSheet(GameObject topSheet)
	{
		float sortHeight = 0.01f;
		papers.Remove(topSheet);
		papers.OrderBy(x => -x.transform.position.z);
		papers.Add(topSheet);
		foreach(GameObject obj in papers)
		{
			if(obj == GameManager.instance.focus.FocusedPaper?.gameObject)
			{
				continue;
			}
			Vector3 newPos = obj.transform.localPosition;
			if(obj.GetComponent<Paper>().GetInfo().attachedPapers != null)
			{
				foreach(GameObject paper in obj.GetComponent<Paper>().GetInfo().attachedPapers)
				{
					sortHeight += 0.001f;
					newPos.z = startingHeight - sortHeight;
					paper.transform.localPosition = newPos;
				}
			}
			sortHeight += 0.001f;
			newPos.z = startingHeight - sortHeight;
			obj.transform.localPosition = newPos;
		}
	}

	public void SortList(GameObject topSheet)
	{
		if(topSheet == tempSortCheck)
		{
			return;
		}
		for(int i = 0; i < papers.Count; i++)
		{
			if(papers[i] == topSheet)
			{
				Vector3 newPos = papers[i].transform.localPosition;
				newPos.z = startingHeight - (float)(papers.Count / 100f);
				papers[i].transform.localPosition = newPos;
				tempSortCheck = papers[i];
			}
			else
			{
				Vector3 newPos = papers[i].transform.localPosition;
				if(newPos.z >= startingHeight)
				{
					continue;
				}
				else
				{
					newPos.z += 0.01f;
				}
				papers[i].transform.localPosition = newPos;
			}
		}
	}
}
