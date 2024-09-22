using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public enum ItemType
    {
        Rock,
        Gold
    }

    public ItemType itemType; // Type of the item to pick up
    public int amount = 1; // Amount to add to player's inventory

    private bool isPlayerInRange = false; // tracks if the player is in range
    private PlayerBehavior player; // ref to the player

    private void Start()
    {
        // Assign random values for rock amount
        if (itemType == ItemType.Rock)
        {
            amount = Random.Range(1, 5); // random value for rocks
        }
        else if (itemType == ItemType.Gold)
        {
            amount = Random.Range(10, 30); // random value for gold
        }

        Debug.Log($"{itemType} pickup created with amount: {amount}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Player is in range
            player = other.GetComponent<PlayerBehavior>(); // Store the player reference
            Debug.Log("Player entered pickup range.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Player has exited the range
            player = null; // Clear player reference
            Debug.Log("Player exited pickup range.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F)) // Check for 'F' key
        {
            Debug.Log("Attempting to collect item...");

            if (player != null)
            {
                switch (itemType)
                {
                    case ItemType.Rock:
                        player.AddAmmo(amount); // Method to add ammo in PlayerBehavior
                        break;
                    case ItemType.Gold:
                        player.AddGold(amount); // Method to add gold in PlayerBehavior
                        break;
                }

                // Update the UI
                GameUI.instance.UpdateAmmoText(); // Update ammo UI
                GameUI.instance.UpdateGoldText(player.gold); // Update gold UI

                // Destroy the pickup item after collection
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player reference is null!");
            }
        }
    }
}
