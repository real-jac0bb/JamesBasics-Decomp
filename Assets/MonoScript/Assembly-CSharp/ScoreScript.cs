using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	public GameObject scoreText;

	public TMP_Text text;

	private void Start()
	{
		if (PlayerPrefs.GetString("CurrentMode") == "endless")
		{
			scoreText.SetActive(true);
			text.text = "Score:\n" + PlayerPrefs.GetInt("CurrentBooks") + " Notebooks";
		}
	}

	private void Update()
	{
	}
}
