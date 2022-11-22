using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
	public Slider slider;

	public Toggle rumble;

	public Toggle analog;

	private void Start()
	{
		if (PlayerPrefs.HasKey("OptionsSet"))
		{
			slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{
				rumble.isOn = true;
			}
			else
			{
				rumble.isOn = false;
			}
			if (PlayerPrefs.GetInt("AnalogMove") == 1)
			{
				analog.isOn = true;
			}
			else
			{
				analog.isOn = false;
			}
		}
		else
		{
			PlayerPrefs.SetInt("OptionsSet", 1);
		}
	}

	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
		if (rumble.isOn)
		{
			PlayerPrefs.SetInt("Rumble", 1);
		}
		else
		{
			PlayerPrefs.SetInt("Rumble", 0);
		}
		if (analog.isOn)
		{
			PlayerPrefs.SetInt("AnalogMove", 1);
		}
		else
		{
			PlayerPrefs.SetInt("AnalogMove", 0);
		}
	}
}
