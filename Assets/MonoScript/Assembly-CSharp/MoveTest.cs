using UnityEngine;

public class MoveTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = base.transform.position + new Vector3(0.1f, 0f, 0f);
	}
}
