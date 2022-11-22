using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
	public bool db;

	public Transform player;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	public float coolDown;

	private NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		Wander();
	}

	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = player.position - base.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, direction, out hitInfo, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player"))
		{
			db = true;
			TargetPlayer();
			return;
		}
		db = false;
		if ((agent.velocity.magnitude <= 1f) & (coolDown <= 0f))
		{
			Wander();
		}
	}

	private void Wander()
	{
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
	}
}
