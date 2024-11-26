using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class audioManager : MonoBehaviour
{
	
	[SerializeField] EventReference paperCrunch;

    public void PaperCrunch()
	{
		Debug.Log("Playing paperCrunch");
		RuntimeManager.PlayOneShot(paperCrunch);
	}
}
