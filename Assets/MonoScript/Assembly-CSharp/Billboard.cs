using UnityEngine;

public class Billboard : MonoBehaviour
{
	private Camera m_Camera;

	private void Start()
	{
		m_Camera = Camera.main;
	}

	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + m_Camera.transform.rotation * Vector3.forward);
	}
}
