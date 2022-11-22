using System.Collections;
using UnityEngine;

public class DefaultSettingsScript : MonoBehaviour
{
	public GameObject options;

	public Canvas canvas;

	private void Start()
	{
		if (!PlayerPrefs.HasKey("OptionsSet"))
		{
			options.SetActive(true);
			StartCoroutine(CloseOptions());
			canvas.enabled = false;
		}
	}

	public IEnumerator CloseOptions()
	{
		yield return new WaitForEndOfFrame();
		canvas.enabled = true;
		options.SetActive(false);
	}
}
