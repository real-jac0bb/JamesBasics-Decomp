using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
	public GameControllerScript gc;

	private void Update()
	{
		if ((EventSystem.current.currentSelectedGameObject == null))
		{
			if (!gc.mouseLocked)
			{
				gc.LockMouse();
			}
		}
		else if (gc.mouseLocked)
		{
			gc.UnlockMouse();
		}
	}
}
