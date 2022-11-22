using UnityEngine;

public class NeedMoreScript : MonoBehaviour
{
	public GameControllerScript gc;

	public AudioSource audioDevice;

	public AudioClip baldiDoor;

	private void OnTriggerEnter(Collider other)
	{
		if ((gc.notebooks < 2) & (other.tag == "Player"))
		{
			audioDevice.PlayOneShot(baldiDoor, 1f);
		}
	}
}
