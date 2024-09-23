using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    public float distanceToGround = 0.1f;
    public int maxHealth = 100;
    public int currentHealth;

    public LayerMask groundLayer;

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer mr;
    public PlayerRocks rocks; // ref to PlayerRocks

    [Header("Pickups")]
    public int gold = 0;
    public bool hasTraded = false; // tracks if the player has traded with the NPC

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        GameUI.instance.Initialize();
        //rig.isKinematic = true;
        currentHealth = maxHealth; // give player max health at the start
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); //update health ui
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();

        if (Input.GetMouseButtonDown(1)) // right mouse button down (button index 1)
            rocks.TryShoot();
    }

    void Move()
    {
        //get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        //set that as our velocity
        rig.velocity = dir;
    }

    void TryJump()
    {
        //Debug.Log("Jumping");

        // create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        // shoot the raycast
        if (Physics.Raycast(ray, 6.0f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void ChangeAmmo(int amount)
    {
        // change ammo
        rocks.curAmmo += amount;

        // ammo doesn't exceed max
        rocks.curAmmo = Mathf.Clamp(rocks.curAmmo, 0, rocks.maxAmmo);

        // update UI
        GameUI.instance.UpdateAmmoText();
    }

    public void AddAmmo(int amount)
    {
        rocks.curAmmo = Mathf.Clamp(rocks.curAmmo + amount, 0, rocks.maxAmmo);
        Debug.Log("Ammo added: " + amount + ". Current ammo: " + rocks.curAmmo);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + ". Total gold: " + gold);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); // update health UI
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); // update health UI
        Debug.Log("Player healed: " + healAmount + ". Current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        GameManager.instance.LoseGame();
    }
}