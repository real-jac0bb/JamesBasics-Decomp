using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerScript : MonoBehaviour
{
	public GameControllerScript gc;

	private void OnTriggerEnter(Collider other)
	{
		if ((gc.notebooks >= 7) & (other.tag == "Player"))
		{
			if (gc.failedNotebooks >= 7)
			{
				SceneManager.LoadScene("Secret");
			}
			else
			{
				SceneManager.LoadScene("Results");
			}
		}
	}
}
