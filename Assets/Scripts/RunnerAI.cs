using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RunnerAI : MonoBehaviour
{

	public NavMeshAgent agent;
	public LayerMask whatIsPotato,whatIsGround;

	//Patrolling 
	public Vector3 walkPoint;
	bool walkPointSet;
	public float walkPointRange;


	//States
	public float sightRange, RunAwayRange;
	public bool CanSeePotatoInRange, CanRunAwayInRange;

	//Running Away
	public GameObject PotatoMan;

	// Start is called before the first frame update
	void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		PotatoMan = GameObject.Find("Potato Man");
    }

	// Update is called once per frame
	void Update()
	{

		CanSeePotatoInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPotato);
		CanRunAwayInRange = Physics.CheckSphere(transform.position, RunAwayRange, whatIsPotato);


		if (!CanSeePotatoInRange && !CanRunAwayInRange)
		{
			Patroling();
		}
		if (CanSeePotatoInRange && CanRunAwayInRange)
		{
			RunAway();
		}
	}

	private void Patroling()
	{
		if (!walkPointSet)
		{
			SearchWalkPoint();
		}

		if (walkPointSet)
		{
			agent.SetDestination(walkPoint);
		}

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		//Walkpoint Reached
		if (distanceToWalkPoint.magnitude < 1f)
		{
			walkPointSet = false;
		}
	}

	private void SearchWalkPoint()
	{
		//Calculate random point in range
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		//Shoots raycast down the position of the set way point and then goes to it.
		if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
		{
			walkPointSet = true;
		}
	}

	private void RunAway()
	{
		agent.SetDestination(new Vector3(PotatoMan.transform.position.z - transform.position.z, transform.position.y, PotatoMan.transform.position.x - transform.position.x));
	}
}
