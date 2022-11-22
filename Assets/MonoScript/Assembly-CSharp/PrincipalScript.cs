using UnityEngine;
using UnityEngine.AI;

public class PrincipalScript : MonoBehaviour
{
	public bool seesRuleBreak;

	public Transform player;

	public Transform bully;

	public bool bullySeen;

	public PlayerScript playerScript;

	public BullyScript bullyScript;

	public BaldiScript baldiScript;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	public DoorScript officeDoor;

	public float coolDown;

	public float timeSeenRuleBreak;

	public bool angry;

	public bool inOffice;

	private int detentions;

	private int[] lockTime = new int[5] { 15, 30, 45, 60, 99 };

	public AudioClip[] audTimes = new AudioClip[5];

	public AudioClip[] audScolds = new AudioClip[3];

	public AudioClip audDetention;

	public AudioClip audNoDrinking;

	public AudioClip audNoBullying;

	public AudioClip audNoFaculty;

	public AudioClip audNoLockers;

	public AudioClip audNoRunning;

	public AudioClip audNoStabbing;

	public AudioClip audNoEscaping;

	public AudioClip aud_Whistle;

	public AudioClip aud_Delay;

	private NavMeshAgent agent;

	private AudioQueueScript audioQueue;

	private AudioSource audioDevice;

	public AudioSource quietAudioDevice;

	private RaycastHit hit;

	private Vector3 aim;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioQueue = GetComponent<AudioQueueScript>();
		audioDevice = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (seesRuleBreak)
		{
			timeSeenRuleBreak += 1f * Time.deltaTime;
			if (((double)timeSeenRuleBreak >= 0.5) & !angry)
			{
				angry = true;
				seesRuleBreak = false;
				timeSeenRuleBreak = 0f;
				TargetPlayer();
				CorrectPlayer();
			}
		}
		else
		{
			timeSeenRuleBreak = 0f;
		}
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (!angry)
		{
			aim = player.position - base.transform.position;
			if (Physics.Raycast(base.transform.position, aim, out hit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & (hit.transform.tag == "Player") & (playerScript.guilt > 0f) & !inOffice & !angry)
			{
				seesRuleBreak = true;
			}
			else
			{
				seesRuleBreak = false;
				if ((agent.velocity.magnitude <= 1f) & (coolDown <= 0f))
				{
					Wander();
				}
			}
			aim = bully.position - base.transform.position;
			if (Physics.Raycast(base.transform.position, aim, out hit, float.PositiveInfinity, 769) & (hit.transform.name == "Its a Bully") & (bullyScript.guilt > 0f) & !inOffice & !angry)
			{
				TargetBully();
			}
		}
		else
		{
			TargetPlayer();
		}
	}

	private void Wander()
	{
		playerScript.principalBugFixer = 1;
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		if (agent.isStopped)
		{
			agent.isStopped = false;
		}
		coolDown = 1f;
		if (Random.Range(0f, 10f) <= 1f)
		{
			quietAudioDevice.PlayOneShot(aud_Whistle);
		}
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
	}

	private void TargetBully()
	{
		if (!bullySeen)
		{
			agent.SetDestination(bully.position);
			audioQueue.QueueAudio(audNoBullying);
			bullySeen = true;
		}
	}

	private void CorrectPlayer()
	{
		audioQueue.ClearAudioQueue();
		if (playerScript.guiltType == "faculty")
		{
			audioQueue.QueueAudio(audNoFaculty);
		}
		else if (playerScript.guiltType == "running")
		{
			audioQueue.QueueAudio(audNoRunning);
		}
		else if (playerScript.guiltType == "drink")
		{
			audioQueue.QueueAudio(audNoDrinking);
		}
		else if (playerScript.guiltType == "escape")
		{
			audioQueue.QueueAudio(audNoEscaping);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			inOffice = true;
		}
		if ((other.tag == "Player") & angry & !inOffice)
		{
			inOffice = true;
			playerScript.principalBugFixer = 0;
			agent.Warp(new Vector3(10f, 0f, 170f));
			agent.isStopped = true;
			other.transform.position = new Vector3(10f, 4f, 160f);
			other.transform.LookAt(new Vector3(base.transform.position.x, other.transform.position.y, base.transform.position.z));
			audioQueue.QueueAudio(aud_Delay);
			audioQueue.QueueAudio(audTimes[detentions]);
			audioQueue.QueueAudio(audDetention);
			int num = Mathf.RoundToInt(Random.Range(0f, 2f));
			audioQueue.QueueAudio(audScolds[num]);
			officeDoor.LockDoor(lockTime[detentions]);
			baldiScript.Hear(base.transform.position, 8f);
			coolDown = 5f;
			angry = false;
			detentions++;
			if (detentions > 4)
			{
				detentions = 4;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			inOffice = false;
		}
		if (other.name == "Its a Bully")
		{
			bullySeen = false;
		}
	}
}
