using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastCheck : MonoBehaviour
{
	private List<GraphicRaycaster> _graphicRaycasters;
	private PointerEventData _clickData;
	private List<RaycastResult> _raycastResults;

	void Start()
	{
		_graphicRaycasters = GameManager.instance.raycasters.retrieve;
        _clickData = new PointerEventData(EventSystem.current);
        _raycastResults = new List<RaycastResult>();
	}

	public bool UI()
	{
		_clickData.position = Input.mousePosition;
		_raycastResults.Clear();
		foreach(GraphicRaycaster raycaster in _graphicRaycasters)
		{
			raycaster.Raycast(_clickData, _raycastResults);
		}

		return _raycastResults.Count > 0;
	}

	public List<GameObject> Objects()
	{
		List<GameObject> output = new();
		RaycastHit[] hits;
		_clickData.position = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll(ray, 100f);
		if(hits == null)
		{
			return null;
		}
		foreach(RaycastHit hit in hits)
		{
			output.Add(hit.transform.gameObject);
		}
		return output;
	}
}
