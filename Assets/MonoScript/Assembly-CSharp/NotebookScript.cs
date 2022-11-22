using UnityEngine;

public class NotebookScript : MonoBehaviour
{
	public float openingDistance;

	public GameControllerScript gc;

	public BaldiScript bsc;

	public float respawnTime;

	public bool up;

	public Transform player;

	public GameObject learningGame;

	public AudioSource audioDevice;

	private void Start()
	{
		up = true;
	}

	private void Update()
	{
		if (gc.mode == "endless")
		{
			if (respawnTime > 0f)
			{
				if ((base.transform.position - player.position).magnitude > 60f)
				{
					respawnTime -= Time.deltaTime;
				}
			}
			else if (!up)
			{
				base.transform.position = new Vector3(base.transform.position.x, 4f, base.transform.position.z);
				up = true;
				audioDevice.Play();
			}
		}
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.transform.tag == "Notebook") & (Vector3.Distance(player.position, base.transform.position) < openingDistance)))
			{
				base.transform.position = new Vector3(base.transform.position.x, -20f, base.transform.position.z);
				up = false;
				respawnTime = 120f;
				gc.CollectNotebook();
				GameObject gameObject = Object.Instantiate(learningGame);
				gameObject.GetComponent<MathGameScript>().gc = gc;
				gameObject.GetComponent<MathGameScript>().baldiScript = bsc;
				gameObject.GetComponent<MathGameScript>().playerPosition = player.position;
			}
		}
	}
}
