using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
	public List<Paper> objectsInTray;

	public virtual void AddToTray(Paper paper)
	{
		if(paper != null)
		{
			objectsInTray.Add(paper);
		}
	}

	public virtual void RemoveFromTray(Paper paper)
	{
		if(paper != null && objectsInTray.Contains(paper))
		{
			objectsInTray.Remove(paper);
		}
	}

	public List<Paper> GetContents()
	{
		return objectsInTray;
	}
}