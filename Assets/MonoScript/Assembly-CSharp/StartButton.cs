using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	public enum Mode
	{
		Story = 0,
		Endless = 1
	}

	public Mode currentMode;

	public void StartGame()
	{
		if (currentMode == Mode.Story)
		{
			PlayerPrefs.SetString("CurrentMode", "story");
		}
		else
		{
			PlayerPrefs.SetString("CurrentMode", "endless");
		}
		SceneManager.LoadSceneAsync("School");
	}
}
