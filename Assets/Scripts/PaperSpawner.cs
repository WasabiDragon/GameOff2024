using System;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
	[SerializeField] GameObject paperPrefab;
	[SerializeField] GameObject inTraySpawn;

	[SerializeField] GameObject printedPaperPrefab;
	[SerializeField] GameObject printedPaperSpawnPoint;

	[SerializeField] GameObject paperParent;

	paperSorter sorter;
	int paperCount = 0;

    public void SpawnPapersInTray(DailyPapers papers)
	{
		sorter = GameManager.instance.paperSorter;
		sorter.SetStartingHeight(inTraySpawn.transform.localPosition.z);

		for(int i = papers.dailyPapers.Count -1; i >= 0; i--)
		{
			SpawnPaper(papers.dailyPapers[i], inTraySpawn, paperPrefab, true);
		}
	}

	private GameObject SpawnPaper(PaperInfo paper, GameObject targetPos, GameObject prefab, bool encrypt = false)
	{
		GameObject obj = Instantiate(prefab, targetPos.transform.position, prefab.transform.rotation);
		Vector3 pos = new Vector3(targetPos.transform.position.x, targetPos.transform.position.y, sorter.AddPaper(obj));

		obj.transform.position = pos;
		obj.name = paper.name;
		obj.transform.parent = paperParent.transform;
		obj.transform.eulerAngles = new Vector3(prefab.transform.rotation.eulerAngles.x, prefab.transform.rotation.eulerAngles.y, targetPos.transform.rotation.eulerAngles.z);
		PaperInfo info = encrypt ? EncryptPaper(paper) : paper;
		obj.GetComponent<Paper>().SetPaper(info);
		sorter.UpdateTopSheet(obj);
		return obj;
	}

	private PaperInfo EncryptPaper(PaperInfo paper)
	{
		paper.displayText = GameManager.instance.decoder.EncryptMessage(paper.encryptionSteps, paper.originalText);
		return paper;
	}

	public void PrintNewPaper(string message)
	{
		PaperInfo paperInfo = ScriptableObject.CreateInstance<PaperInfo>();
		paperInfo.displayText = message;
		paperInfo.printed = true;
		
		GameObject obj = SpawnPaper(paperInfo, printedPaperSpawnPoint, printedPaperPrefab);

		obj.name = "printedPaper_"+paperCount.ToString();
		paperCount +=1;

	}

	internal void Respawn(List<GameObject> objects)
	{
		foreach(GameObject obj in objects)
		{
			SpawnPaper(obj.GetComponent<Paper>().GetInfo(), inTraySpawn, paperPrefab, true);
		}
	}
}
