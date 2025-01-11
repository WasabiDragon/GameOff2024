using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicRaycastersList : MonoBehaviour
{
    [SerializeField] private List<GraphicRaycaster> raycasters;

	public List<GraphicRaycaster> retrieve
	{
		get
		{
			return raycasters;
		}
	}
}
