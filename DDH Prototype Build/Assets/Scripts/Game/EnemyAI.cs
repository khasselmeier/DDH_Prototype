using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float chaseRange = 10f;      // The range within which the enemy will start chasing the player
    public float stopChaseRange = 15f;  // The range at which enemy will stop chasing if player moves away
    public float distanceToPlayer;       // The distance between the enemy and the player

    public Transform[] patrolPoints;     // Array of patrol points
    public float patrolSpeed = 2f;       // Speed during patrol
    public float waitTimeAtPatrolPoint = 2f; // Time to wait at each patrol point

    public int maxHealth = 3;            // Max health of the enemy
    private int currentHealth;            // Current health of the enemy

    private NavMeshAgent navMeshAgent;   // Reference to the NavMeshAgent component
    private Transform player;            // The player's transform
    private bool isChasing = false;      // Keeps track of whether the enemy is currently chasing
    private int currentPatrolIndex;      // Current patrol point index
    private float waitTimer;              // Timer for waiting at patrol points

    void Start()
    {
        // Get the NavMeshAgent component attached to the enemy
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0; // Start at the first patrol point
        navMeshAgent.speed = patrolSpeed; // Set the patrol speed
        currentHealth = maxHealth; // Initialize health
    }

    void Update()
    {
        // Find the player if it hasn't been assigned yet
        if (player == null)
        {
            FindPlayer();
            return;
        }

        // Calculate the distance between the enemy and the player
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Start chasing if within chase range and not already chasing
        if (distanceToPlayer <= chaseRange && !isChasing)
        {
            StartChasing();
        }
        // Stop chasing if the player moves outside the stop chase range
        else if (distanceToPlayer > stopChaseRange && isChasing)
        {
            StopChasing();
        }

        // If chasing, set the destination to the player's position
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            Patrol(); // Patrol if not chasing
        }
    }

    private void Patrol()
    {
        // Check if the enemy has reached the current patrol point
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            waitTimer += Time.deltaTime; // Start waiting at the patrol point

            if (waitTimer >= waitTimeAtPatrolPoint)
            {
                // Move to the next patrol point
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Loop back to the start
                navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position); // Set the new destination
                waitTimer = 0; // Reset the wait timer
            }
        }
        else
        {
            waitTimer = 0; // Reset wait timer if moving
        }
    }

    private void FindPlayer()
    {
        // Try to find the player GameObject by tag (assumes the player is tagged as "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player found by EnemyAI.");
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player character is tagged as 'Player'.");
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        navMeshAgent.isStopped = false; // Allow the agent to move
        Debug.Log("Enemy has started chasing the player.");
    }

    private void StopChasing()
    {
        isChasing = false;
        navMeshAgent.isStopped = true; // Stop the agent's movement
        Debug.Log("Enemy has stopped chasing the player");
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health is zero or below
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        Debug.Log("Enemy has died.");
        // Here you can add any death animations, effects, or cleanup.
        Destroy(gameObject); // Remove the enemy from the scene
    }
}