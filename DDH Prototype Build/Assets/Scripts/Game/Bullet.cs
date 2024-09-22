using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rig;

    public void Initialize()
    {
        Destroy(gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyAI component and call TakeDamage
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // each bullet does 1 damage
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}