using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
	InputAction _clickAction;
	bool _clicked = false;
	GameObject _interactableCurrent = null;

	void Start()
	{
		_clickAction = InputSystem.actions["Click"];
		_clickAction.performed += IStart;
		_clickAction.canceled += IEnd;
	}

	void Update()
	{
		if (_clicked)
		{
			IDrag();
		} 
	}

	private void IStart(InputAction.CallbackContext context)
    {
        if(HasClickedOverUI())
        {
            return;
        }
		_clicked = true;
        _interactableCurrent = Interact();
		_interactableCurrent?.GetComponent<Interactable>().InteractStart();
    }

	private void IEnd(InputAction.CallbackContext context)
	{
		_clicked = false;
		_interactableCurrent?.GetComponent<Interactable>().InteractEnd();
	}

	private void IDrag()
	{
		_interactableCurrent?.GetComponent<Interactable>().InteractDrag();
	}

	
	public GameObject Interact()
	{
		GameObject topObject = null;
		List<GameObject> objects = GameManager.instance.rays.Objects();
		List<GameObject> nonPaperObjects = new();

		// foreach (GameObject obj in objects)
		// {
		// 	if (obj.GetComponent<Interactable>() != null && obj.GetComponent<PaperInteract>() == null)
		// 	{
		// 		nonPaperObjects.Add(obj);
		// 	}
		// }
		// if (nonPaperObjects.Count > 0)
		// {
		// 	if (nonPaperObjects.Count > 1)
		// 	{
		// 		topObject = GetTopObject(nonPaperObjects);
		// 	}
		// 	else
		// 	{
		// 		topObject = nonPaperObjects[0];
		// 	}
		// }
		// else
		// {
			topObject = GetTopObject(objects);
		// }

		if (topObject != null)
		{
			Debug.Log(topObject.name);
			return topObject;
		}
		Debug.Log("no top object found.");
		return null;
	}

	private GameObject GetTopObject(List<GameObject> objects)
	{
		float _zPos = 0;
		GameObject outputObj = null;
		foreach (GameObject obj in objects)
		{
			if (obj.GetComponent<Interactable>() == null)
			{
				continue;
			}
			if (_zPos == 0)
			{
				_zPos = obj.transform.position.z;
			}
			if (obj.transform.position.z <= _zPos)
			{
				outputObj = obj;
			}
		}
		return outputObj;
	}

	private bool HasClickedOverUI()
    {
        return GameManager.instance.rays.UI();
    }
}
