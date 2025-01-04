using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Tray
{
	public override void AddToTray(Paper paper)
	{
		paperSorter sorter = GameManager.instance.paperSorter;
		RemoveFromTray(paper);
		List<GameObject> importantObjects = new List<GameObject>();
		if(paper.attachedTo != null)
		{
			foreach(GameObject obj in paper.attachedTo.GetComponent<Paper>().GetInfo().attachedPapers)
			{
				if(obj.GetComponent<Paper>().GetInfo().originalPaper)
				{
					importantObjects.Add(obj);
				}
				else
				{
					sorter.RemovePaper(obj);
					Destroy(obj);
				}
			}
		}
		if(paper.GetInfo().attachedPapers != null)
		{
			foreach(GameObject obj in paper.GetInfo().attachedPapers)
			{
				if(obj.GetComponent<Paper>().GetInfo().originalPaper)
				{
					importantObjects.Add(obj);
				}
				else
				{
					sorter.RemovePaper(obj);
					Destroy(obj);
				}
			}
		}
		if(!paper.GetInfo().originalPaper)
		{
			sorter.RemovePaper(paper.gameObject);
			Destroy(paper.gameObject);
		}
		else
		{
			importantObjects.Add(paper.gameObject);
		}

		if(importantObjects != null && importantObjects.Count > 0)
		{
			GameManager.instance.paperSpawner.Respawn(importantObjects);
			foreach(GameObject obj in importantObjects)
			{
				sorter.RemovePaper(obj);
				Destroy(obj);
			}
		}

		
	}
}
