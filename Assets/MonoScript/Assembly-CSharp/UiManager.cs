using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
	public CanvasScaler normScaler;

	public RectTransform[] transforms;

	private void Start()
	{
		int @int = PlayerPrefs.GetInt("UiSize");
		int int2 = PlayerPrefs.GetInt("UiHeight");
		switch (@int)
		{
		case 1:
			normScaler.referenceResolution = new Vector2(640f, 480f);
			break;
		case 2:
			normScaler.referenceResolution = new Vector2(800f, 600f);
			break;
		case 3:
			normScaler.referenceResolution = new Vector2(900f, 720f);
			break;
		case 4:
			normScaler.referenceResolution = new Vector2(1024f, 720f);
			break;
		}
		switch (int2)
		{
		case 1:
		{
			RectTransform[] array2 = transforms;
			foreach (RectTransform rectTransform2 in array2)
			{
				rectTransform2.position = new Vector3(rectTransform2.position.x, rectTransform2.position.y + (float)(Screen.height / 8), rectTransform2.position.z);
			}
			break;
		}
		case 2:
		{
			RectTransform[] array = transforms;
			foreach (RectTransform rectTransform in array)
			{
				rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + (float)(Screen.height / 4), rectTransform.position.z);
			}
			break;
		}
		}
	}
}
