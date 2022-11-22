using UnityEngine;
using UnityEngine.UI;

public class MouseSliderScript : MonoBehaviour
{
	public Slider slider;

	private void Start()
	{
		if (PlayerPrefs.GetFloat("MouseSensitivity") < 100f)
		{
			PlayerPrefs.SetFloat("MouseSensitivity", 200f);
		}
		slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
	}
}
