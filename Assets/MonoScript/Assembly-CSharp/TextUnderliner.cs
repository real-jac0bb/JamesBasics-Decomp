using TMPro;
using UnityEngine;

public class TextUnderliner : MonoBehaviour
{
	public TMP_Text text;

	public void Underline()
	{
		text.fontStyle = FontStyles.Underline;
	}

	public void Ununderline()
	{
		text.fontStyle = FontStyles.Normal;
	}
}
