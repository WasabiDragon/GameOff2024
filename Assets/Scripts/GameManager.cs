using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	void Awake()
	{
		instance = this;
	}
	public Decoder decoder;
	public paperSorter paperSorter;
	public PaperSpawner paperSpawner;
	public FocusMode focus;
	public SnapPoints snapping;
}
