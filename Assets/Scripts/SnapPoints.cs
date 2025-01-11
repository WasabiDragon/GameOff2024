using System.Collections.Generic;
using UnityEngine;

public class SnapPoints : MonoBehaviour
{
	public List<Transform> nodes = new List<Transform>();
	public float snapDistance;
	private GameObject currentSnapPoint;

    public Transform Check(Vector3 currentPoint)
	{
		currentSnapPoint = null;
		float smallestDistance = snapDistance;
		foreach(Transform node in nodes)
		{
			float distance = Vector2.Distance(new Vector2(node.position.x, node.position.y), new Vector2(currentPoint.x, currentPoint.y));
			if( distance < smallestDistance)
			{
				smallestDistance = distance;
				currentSnapPoint = node.gameObject;
				// return node;
			}
		}
		return currentSnapPoint?.transform;
	}

	public GameObject currentSnap
	{
		get
		{
			return currentSnapPoint;
		}
	}
}
