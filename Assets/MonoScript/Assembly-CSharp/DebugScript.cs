using UnityEngine;

public class DebugScript : MonoBehaviour
{
	public bool limitFramerate;

	public int framerate;

	private void Start()
	{
		if (limitFramerate)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = framerate;
		}
	}

	private void Update()
	{
	}
}
