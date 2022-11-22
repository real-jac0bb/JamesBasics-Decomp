using UnityEngine;

public class PlatformSpecificMenu : MonoBehaviour
{
	public GameObject pC;

	public GameObject mobile;

	private void Start()
	{
		pC.SetActive(true);
	}
}
