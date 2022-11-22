using UnityEngine;
using UnityEngine.AI;

public class FirstPrizeScript : MonoBehaviour
{
	public float debug;

	public float turnSpeed;

	public float str;

	public float angleDiff;

	public float normSpeed;

	public float runSpeed;

	public float currentSpeed;

	public float acceleration;

	public float speed;

	public float autoBrakeCool;

	public float crazyTime;

	public Quaternion targetRotation;

	public float coolDown;

	private float prevSpeed;

	public bool playerSeen;

	public bool hugAnnounced;

	public AILocationSelectorScript wanderer;

	public Transform player;

	public Transform wanderTarget;

	public AudioClip[] aud_Found = new AudioClip[2];

	public AudioClip[] aud_Lost = new AudioClip[2];

	public AudioClip[] aud_Hug = new AudioClip[2];

	public AudioClip[] aud_Random = new AudioClip[2];

	public AudioClip audBang;

	public AudioSource audioDevice;

	public AudioSource motorAudio;

	private NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		coolDown = 1f;
		Wander();
	}

	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (autoBrakeCool > 0f)
		{
			autoBrakeCool -= 1f * Time.deltaTime;
		}
		else
		{
			agent.autoBraking = true;
		}
		angleDiff = Mathf.DeltaAngle(base.transform.eulerAngles.y, Mathf.Atan2(agent.steeringTarget.x - base.transform.position.x, agent.steeringTarget.z - base.transform.position.z) * 57.29578f);
		if (crazyTime <= 0f)
		{
			if (Mathf.Abs(angleDiff) < 5f)
			{
				base.transform.LookAt(new Vector3(agent.steeringTarget.x, base.transform.position.y, agent.steeringTarget.z));
				agent.speed = currentSpeed;
			}
			else
			{
				base.transform.Rotate(new Vector3(0f, turnSpeed * Mathf.Sign(angleDiff) * Time.deltaTime, 0f));
				agent.speed = 0f;
			}
		}
		else
		{
			agent.speed = 0f;
			base.transform.Rotate(new Vector3(0f, 180f * Time.deltaTime, 0f));
			crazyTime -= Time.deltaTime;
		}
		motorAudio.pitch = (agent.velocity.magnitude + 1f) * Time.timeScale;
	}

	private void FixedUpdate()
	{
		Vector3 direction = player.position - base.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, direction, out hitInfo, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player"))
		{
			if (!playerSeen && !audioDevice.isPlaying)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(aud_Found[num]);
			}
			playerSeen = true;
			TargetPlayer();
			currentSpeed = runSpeed;
			return;
		}
		currentSpeed = normSpeed;
		if (playerSeen & (coolDown <= 0f))
		{
			if (!audioDevice.isPlaying)
			{
				int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(aud_Lost[num2]);
			}
			playerSeen = false;
			Wander();
		}
		else if ((agent.velocity.magnitude <= 1f) & (coolDown <= 0f) & ((base.transform.position - agent.destination).magnitude < 5f))
		{
			Wander();
		}
	}

	private void Wander()
	{
		wanderer.GetNewTargetHallway();
		agent.SetDestination(wanderTarget.position);
		hugAnnounced = false;
		int num = Mathf.RoundToInt(Random.Range(0f, 9f));
		if ((!audioDevice.isPlaying && num == 0) & (coolDown <= 0f))
		{
			int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
			audioDevice.PlayOneShot(aud_Random[num2]);
		}
		coolDown = 1f;
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 0.5f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!audioDevice.isPlaying & !hugAnnounced)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(aud_Hug[num]);
				hugAnnounced = true;
			}
			agent.autoBraking = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			autoBrakeCool = 1f;
		}
	}

	public void GoCrazy()
	{
		crazyTime = 15f;
	}
}
