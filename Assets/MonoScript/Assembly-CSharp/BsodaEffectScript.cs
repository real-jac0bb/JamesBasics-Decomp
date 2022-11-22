using UnityEngine;
using UnityEngine.AI;

public class BsodaEffectScript : MonoBehaviour
{
	private NavMeshAgent agent;

	private Vector3 otherVelocity;

	private bool inBsoda;

	private float failSave;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (inBsoda)
		{
			agent.velocity = otherVelocity;
		}
		if (failSave > 0f)
		{
			failSave -= Time.deltaTime;
		}
		else
		{
			inBsoda = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "BSODA")
		{
			inBsoda = true;
			otherVelocity = other.GetComponent<Rigidbody>().velocity;
			failSave = 1f;
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			inBsoda = true;
			otherVelocity = base.transform.forward * agent.speed * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			failSave = 1f;
		}
	}

	private void OnTriggerExit()
	{
		inBsoda = false;
	}
}
