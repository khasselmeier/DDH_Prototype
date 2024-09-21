/*using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "player")
        {
            agent.destination = player.position;
            Debug.Log("player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "player")
        {
            Debug.Log("player out of range, resume patrol");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }
        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    public float followDistance = 20f; // Distance within which the enemy will follow the player
    private bool playerInRange = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (playerInRange && distanceToPlayer > followDistance)
        {
            playerInRange = false;
            Debug.Log("Player out of range, resume patrol.");
            MoveToNextPatrolLocation(); // Resume patrol
        }
        else if (!playerInRange && distanceToPlayer <= followDistance)
        {
            playerInRange = true;
            agent.destination = player.position; // Start following the player
            Debug.Log("Player detected - attack!");
        }

        // Continue to follow player if in range
        if (playerInRange)
        {
            agent.destination = player.position;
        }

        // Handle patrol behavior
        if (!playerInRange && agent.remainingDistance < .2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit! Lives remaining: " + EnemyLives);
        }
    }
}
