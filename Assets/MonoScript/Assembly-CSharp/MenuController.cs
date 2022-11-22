using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public UIController uc;

	public Selectable firstButton;

	public Selectable dummyButtonPC;

	public Selectable dummyButtonElse;

	public GameObject back;

	public void OnEnable()
	{
		uc.firstButton = firstButton;
		uc.dummyButtonPC = dummyButtonPC;
		uc.dummyButtonElse = dummyButtonElse;
		uc.SwitchMenu();
	}

	private void Update()
	{
		if (Input.GetButtonDown("UICancel") && back != null)
		{
			back.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}
}
