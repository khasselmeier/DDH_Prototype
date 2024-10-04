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
    public int npcHitDamage = 50;

    public LayerMask groundLayer;

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer mr;
    public PlayerRocks rocks;
    public GameObject pickaxe;

    [Header("Pickups")]
    public int gold = 0;
    public bool hasTraded = false; // tracks if the player has traded with the NPC
    public bool canMineBaseGem = false; // player must trade once to mine these gems
    public bool canMineHighGem = false; // player must trade twice to mine these gems
    public static int collectedGems = 0;
    public int totalValueOfGems = 0; // quota collected

    private void Start()
    {
        Initialize();

        if (pickaxe != null)
        {
            pickaxe.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Initialize()
    {
        GameUI.instance.Initialize();
        currentHealth = maxHealth; // give player max health at the start
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); // update health ui
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();

        if (Input.GetMouseButtonDown(1)) // right mouse button down (button index 1)
            rocks.TryShoot();

        if (Input.GetMouseButtonDown(0)) // left mouse button down (button index 0)
            TryMine();
    }

    void Move()
    {
        // get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        // set that as our velocity
        rig.velocity = dir;
    }

    void TryJump()
    {
        // create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        // shoot the raycast
        if (Physics.Raycast(ray, 6.0f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //Debug.Log("Jumping");
    }

    public void TryMine()
    {
        RaycastHit hit;

        // vizualize the raycast in the editor (to help with some gems not being mined)
        Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.red, 1.0f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
        {
            GameObject gem = hit.collider.gameObject;
            //Debug.Log($"Hit: {gem.name}, Tag: {gem.tag}");

            if (gem.CompareTag("BaseGem") && canMineBaseGem)
            {
                CollectGem(gem, "BaseGem");
            }
            else if (gem.CompareTag("HighGem") && canMineHighGem)
            {
                CollectGem(gem, "HighGem");
            }
            else
            {
                Debug.Log("No gem detected in front of the player"); // nothing was hit by raycast
            }
        }
    }

    public void CollectGem(GameObject gem, string gemType)
    {
        GemPickup gemPickup = gem.GetComponent<GemPickup>();
        if (gemPickup != null)
        {
            totalValueOfGems += gemPickup.gemValue;
            Destroy(gem);
            collectedGems += 1;

            Debug.Log($"Player mined a {gemType}! Total collected: {collectedGems}, Total Value: {totalValueOfGems}");

            // update UI
            GameUI.instance.UpdateGemsValueText(totalValueOfGems);
        }
        else
        {
            Debug.LogError("GemPickup component not found on gem.");
        }
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
        //Debug.Log("Ammo added: " + amount + ". Current ammo: " + rocks.curAmmo);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        //Debug.Log("Gold added: " + amount + ". Total gold: " + gold);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); // update health UI
        //Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        GameUI.instance.UpdateHealthText(currentHealth, maxHealth); // update health UI
        //Debug.Log("Player healed: " + healAmount + ". Current health: " + currentHealth);
    }

    private void Die()
    {
        //Debug.Log("Player has died.");
        GameManager.instance.LoseGame();
    }

    public void OnNpcHit()
    {
        Debug.Log("The NPC doesn't enjoy being hit");
        TakeDamage(npcHitDamage); // player takes damage when hitting the NPC
    }
}