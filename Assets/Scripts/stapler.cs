using System.Collections.Generic;
using UnityEngine;


public class Stapler : Interactable
{
	public GameObject buttons;
	Vector3 startMousePos;
	[ReadOnly] public List<GameObject> collisions;
	// bool clicked = false;

	void Start()
	{	
		collisions = new List<GameObject>();
	}

	public override void InteractStart()
	{
		startMousePos = Input.mousePosition;
		// clicked = true;
	}


	public override void InteractEnd()
	{
		if(Input.mousePosition == startMousePos)
		{
			buttons.SetActive(true);
		}
		// clicked = false;
	}


	void OnTriggerEnter(Collider other) => collisions.Add(other.gameObject);

	void OnTriggerExit(Collider other) => collisions.Remove(other.gameObject);

	public void Bind()
	{
		buttons.SetActive(false);
		GameObject original = collisions.Find(x => x.GetComponent<Paper>().GetInfo().originalPaper == true);
		Paper originalInfo = original.GetComponent<Paper>();
		float rot = 5f;
		List<GameObject> tempCollisions = new List<GameObject>(collisions);
		foreach(GameObject obj in tempCollisions)
		{
			if(obj != original)
			{
				GameManager.instance.paperSorter.RemovePaper(obj);
				originalInfo.AttachPaper(obj);
				obj.transform.eulerAngles = new Vector3(original.transform.eulerAngles.x, original.transform.eulerAngles.y, original.transform.eulerAngles.z+rot);
				rot +=5f;
				Vector3 pos = new Vector3(original.transform.position.x, original.transform.position.y, original.transform.position.z + (rot/5000f));
				obj.transform.position = pos;
				obj.GetComponent<PaperInteract>().ParentedToOther = true;
			}
		}
	}

	public void Unbind()
	{
		buttons.SetActive(false);
		GameObject original = collisions.Find(x => x.GetComponent<Paper>().GetInfo().originalPaper == true);
		original.GetComponent<Paper>().DetachAll();
	}
}
