using UnityEngine;
using UnityEngine.AI;

public class CraftersScript : MonoBehaviour
{
	public bool db;

	public bool angry;

	public bool gettingAngry;

	public float anger;

	private float forceShowTime;

	public Transform player;

	public Transform playerCamera;

	public Transform baldi;

	public NavMeshAgent baldiAgent;

	public GameObject sprite;

	public GameControllerScript gc;

	[SerializeField]
	private NavMeshAgent agent;

	public Renderer craftersRenderer;

	public SpriteRenderer spriteImage;

	public Sprite angrySprite;

	private AudioSource audioDevice;

	public AudioClip aud_Intro;

	public AudioClip aud_Loop;

	private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
		sprite.SetActive(false);
	}

	private void Update()
	{
		if (forceShowTime > 0f)
		{
			forceShowTime -= Time.deltaTime;
		}
		if (gettingAngry)
		{
			anger += Time.deltaTime;
			if ((anger >= 1f) & !angry)
			{
				angry = true;
				audioDevice.PlayOneShot(aud_Intro);
				spriteImage.sprite = angrySprite;
			}
		}
		else if (anger > 0f)
		{
			anger -= Time.deltaTime;
		}
		if (!angry)
		{
			if ((((base.transform.position - agent.destination).magnitude <= 20f) & ((base.transform.position - player.position).magnitude >= 60f)) || forceShowTime > 0f)
			{
				sprite.SetActive(true);
			}
			else
			{
				sprite.SetActive(false);
			}
			return;
		}
		agent.speed += 60f * Time.deltaTime;
		TargetPlayer();
		if (!audioDevice.isPlaying)
		{
			audioDevice.PlayOneShot(aud_Loop);
		}
	}

	private void FixedUpdate()
	{
		if (gc.notebooks >= 7)
		{
			Vector3 direction = player.position - base.transform.position;
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out hitInfo, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player") & craftersRenderer.isVisible & sprite.activeSelf)
			{
				gettingAngry = true;
			}
			else
			{
				gettingAngry = false;
			}
		}
	}

	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!angry && agent.isActiveAndEnabled)
		{
			agent.SetDestination(location);
			if (flee)
			{
				forceShowTime = 3f;
			}
		}
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Player") & angry)
		{
			player.position = new Vector3(5f, player.position.y, 80f);
			baldiAgent.Warp(new Vector3(5f, baldi.position.y, 125f));
			player.LookAt(new Vector3(baldi.position.x, player.position.y, baldi.position.z));
			gc.DespawnCrafters();
		}
	}
}
