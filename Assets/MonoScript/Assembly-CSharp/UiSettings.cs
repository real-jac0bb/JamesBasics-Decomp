using UnityEngine;
using UnityEngine.UI;

public class UiSettings : MonoBehaviour
{
	public Toggle sAuto;

	public Toggle sXLarge;

	public Toggle sLarge;

	public Toggle sMed;

	public Toggle sSmall;

	public Toggle hLow;

	public Toggle hMed;

	public Toggle hHigh;

	private int size;

	private int height;

	public void UpdateState()
	{
		if (sAuto.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 0);
		}
		else if (sXLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 1);
		}
		else if (sLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 2);
		}
		else if (sMed.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 3);
		}
		else if (sSmall.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 4);
		}
		if (hLow.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 0);
		}
		else if (hMed.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 1);
		}
		else if (hHigh.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 2);
		}
	}

	public void RestoreState()
	{
		size = PlayerPrefs.GetInt("UiSize");
		height = PlayerPrefs.GetInt("UiHeight");
		if (size == 0)
		{
			sAuto.isOn = true;
		}
		else if (size == 1)
		{
			sXLarge.isOn = true;
		}
		else if (size == 2)
		{
			sLarge.isOn = true;
		}
		else if (size == 3)
		{
			sMed.isOn = true;
		}
		else if (size == 4)
		{
			sSmall.isOn = true;
		}
		if (height == 0)
		{
			hLow.isOn = true;
		}
		else if (height == 1)
		{
			hMed.isOn = true;
		}
		else if (height == 2)
		{
			hHigh.isOn = true;
		}
	}
}
