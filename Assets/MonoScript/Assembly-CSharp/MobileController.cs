using UnityEngine;

public class MobileController : MonoBehaviour
{
	public GameObject simpleControls;

	public GameObject proControls;

	private bool active;

	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (InputTypeManager.usingTouch)
		{
			if (!active)
			{
				ActivateMobileControls();
			}
		}
		else if (active)
		{
			DeactivateMobileControls();
		}
	}

	private void ActivateMobileControls()
	{
		simpleControls.SetActive(true);
		active = true;
	}

	private void DeactivateMobileControls()
	{
		proControls.SetActive(false);
		simpleControls.SetActive(false);
		active = false;
	}
}
