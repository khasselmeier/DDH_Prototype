using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float chaseRange = 10f;
    public float stopChaseRange = 15f;
    public float distanceToPlayer; // distance between the enemy and the player

    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float waitTimeAtPatrolPoint = 2f;

    public int maxHealth = 3;
    private int currentHealth;

    private NavMeshAgent navMeshAgent;
    private Transform player;
    private bool isChasing = false;
    private int currentPatrolIndex;
    private float waitTimer;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0; // start at the first patrol point
        navMeshAgent.speed = patrolSpeed;
        currentHealth = maxHealth; // initialize health
    }

    void Update()
    {
        // find the player (will keep updating until player is found)
        if (player == null)
        {
            FindPlayer();
            return;
        }

        // calculates the distance between the enemy and the player
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // chase if within chase range and not already chasing
        if (distanceToPlayer <= chaseRange && !isChasing)
        {
            StartChasing();
        }
        // stop chasing if the player moves outside the stop chase range
        else if (distanceToPlayer > stopChaseRange && isChasing)
        {
            StopChasing();
        }

        // set the destination to the players position if enemy is chasing
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            waitTimer += Time.deltaTime; // wait at the patrol point

            if (waitTimer >= waitTimeAtPatrolPoint)
            {
                // move to the next patrol point
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Loop back to the start
                navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position); // Set the new destination
                waitTimer = 0; // reset the wait timer
            }
        }
        else
        {
            waitTimer = 0; // resets the wait timer if moving
        }
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player found by EnemyAI");
        }
        else
        {
            Debug.LogError("Player not found by EnemyAI");
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        navMeshAgent.isStopped = false;
        Debug.Log("Enemy has started chasing the player");
    }

    private void StopChasing()
    {
        isChasing = false;
        navMeshAgent.isStopped = true;
        Debug.Log("Enemy has stopped chasing the player");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        // calls the die method if health hits zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        // add death animation/effect here
        Destroy(gameObject);
    }
}