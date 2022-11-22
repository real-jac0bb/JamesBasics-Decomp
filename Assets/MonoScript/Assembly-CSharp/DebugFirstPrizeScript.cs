using UnityEngine;

public class DebugFirstPrizeScript : MonoBehaviour
{
	public Transform player;

	public Transform first;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = first.position + new Vector3(Mathf.RoundToInt(first.forward.x), 0f, Mathf.RoundToInt(first.forward.z)) * 3f;
	}
}
