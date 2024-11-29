using System.Collections.Generic;
using System.ComponentModel;
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
		foreach(GameObject sheet in papers)
		{
			if(sheet == paper)
			{
				papers.Remove(sheet);
			}
		}
	}

	public void SetStartingHeight(float pos)
	{
		startingHeight = pos;
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
				newPos.z += 0.01f;
				papers[i].transform.localPosition = newPos;
			}
		}
	}
}
