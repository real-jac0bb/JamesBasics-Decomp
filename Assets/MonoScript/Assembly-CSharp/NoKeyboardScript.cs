using UnityEngine;
using UnityEngine.UI;

public class NoKeyboardScript : InputField
{
	protected override void Start()
	{
		base.keyboardType = (TouchScreenKeyboardType)(-1);
		base.Start();
	}
}
