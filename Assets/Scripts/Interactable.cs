using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	public bool clicked = false;

	public virtual void InteractStart()
	{
		return;
	}

	public virtual void InteractDrag()
	{
		return;
	}

	public virtual void InteractEnd()
	{
		return;
	}
}
