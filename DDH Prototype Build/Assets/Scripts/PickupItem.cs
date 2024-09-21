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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();
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

                // update the UI
                GameUI.instance.UpdateAmmoText(); // Update ammo UI
                GameUI.instance.UpdateGoldText(player.gold); // Update gold UI

                // destroy the pickup item after collection
                Destroy(gameObject);
            }
        }
    }
}
