using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAttack : MonoBehaviour
{
    [Header("Chase and Attack Settings")]
    public float detectionRange = 15f;
    public float stopChaseRange = 25f;
    public float attackRange = 10f;
    public float chaseSpeed = 10f;
    public float patrolSpeed = 15f;
    public float timeBetweenAttacks = 1f;

    [Header("Other Settings")]
    public int maxHealth = 10;
    private int currentHealth;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    //private bool isPlayerInRange;
    private bool isChasing = false;
    private bool alreadyAttacked;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            StartChasing();
        }

        if (distanceToPlayer > stopChaseRange && isChasing)
        {
            StopChasing();
        }

        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange && !alreadyAttacked)
            {
                AttackPlayer();
            }
        }
    }

    // Method to make the enemy attack the player
    private void AttackPlayer()
    {
        // Implement attack logic, e.g., reduce player's health
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.TakeDamage(10); // Deal 20 damage to the player
            Debug.Log("Enemy attacked the player!");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack after a delay
        }
    }

    // Resets the attack status to allow for the next attack
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Start chasing the player
    private void StartChasing()
    {
        if (!isChasing)
        {
            isChasing = true;
            navMeshAgent.speed = chaseSpeed;
            Debug.Log("Enemy has started chasing the player.");
        }
    }

    // Stop chasing the player
    private void StopChasing()
    {
        isChasing = false;
        navMeshAgent.speed = patrolSpeed; // Return to patrol speed when not chasing
        Debug.Log("Enemy has stopped chasing the player.");
    }

    // Method to find the player in the scene
    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found by EnemyChaseAttack script.");
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Enemy dies
    private void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject);
    }

    // Gizmos to show detection and attack ranges in the editor
    private void OnDrawGizmosSelected()
    {
        // Detection range (chasing)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Stop chase range (larger range to stop chasing)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}