using UnityEngine;

public class DarkDoorScript : MonoBehaviour
{
	public SwingingDoorScript door;

	public Material lightDoo0;

	public Material lightDoo60;

	public Material lightDooLock;

	public MeshRenderer mesh;

	private void Update()
	{
		if (door.bDoorOpen)
		{
			mesh.material = lightDoo60;
		}
		else if (door.bDoorLocked)
		{
			mesh.material = lightDooLock;
		}
		else
		{
			mesh.material = lightDoo0;
		}
	}
}
