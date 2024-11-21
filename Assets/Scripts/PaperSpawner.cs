using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
	[SerializeField] GameObject paperPrefab;
	[SerializeField] GameObject inTraySpawn;

	[SerializeField] GameObject printedPaperPrefab;
	[SerializeField] GameObject printedPaperSpawnPoint;

	paperSorter sorter;

    public void SpawnPapersInTray(DailyPapers papers)
	{
		sorter = GameManager.instance.paperSorter;
		sorter.SetStartingHeight(inTraySpawn.transform.localPosition.z);

		foreach(PaperInfo paper in papers.dailyPapers)
		{
			GameObject obj = Instantiate(paperPrefab, inTraySpawn.transform.position, paperPrefab.transform.rotation);
			Vector3 pos = new Vector3(inTraySpawn.transform.position.x, inTraySpawn.transform.position.y, sorter.AddPaper(obj));
			obj.transform.parent = inTraySpawn.transform;
			obj.transform.eulerAngles = new Vector3(paperPrefab.transform.rotation.eulerAngles.x, paperPrefab.transform.rotation.eulerAngles.y, inTraySpawn.transform.rotation.eulerAngles.z);
			Paper objpap = obj.GetComponent<Paper>();
			objpap.SetPaper(paper);
			sorter.SortList(obj);
		}
	}

	public void PrintNewPaper(string message)
	{
		GameObject obj = Instantiate(printedPaperPrefab, printedPaperSpawnPoint.transform.position, printedPaperPrefab.transform.rotation);
		Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y, sorter.AddPaper(obj));
		obj.transform.position = pos;
		obj.transform.parent = inTraySpawn.transform;

		Paper objpap = obj.GetComponent<Paper>();
		PaperInfo paperInfo = ScriptableObject.CreateInstance<PaperInfo>();
		paperInfo.paperText = message;
		objpap.SetPaper(paperInfo);
		sorter.SortList(obj);
	}
}
