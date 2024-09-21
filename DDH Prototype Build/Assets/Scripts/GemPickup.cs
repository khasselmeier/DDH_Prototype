using UnityEngine;

public class GemPickup : MonoBehaviour
{
    private const int totalGems = 6; // total number of gems needed to win (constant number)
    public static int collectedGems; // number of gems collected
    private bool isPlayerInRange = false; // tracks if the player is in range
    private PlayerBehavior player; // ref to the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Player is in range
            Debug.Log("Player entered gem pickup range.");
            player = other.GetComponent<PlayerBehavior>(); // Store the player reference
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Player has exited the range
            player = null; // Clear player reference
            Debug.Log("Player exited gem pickup range.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Attempting to collect gem...");
            if (player != null)
            {
                Debug.Log("Player has traded: " + player.hasTraded); // Check trade status
                if (player.hasTraded) // Check if the player has traded with the NPC
                {
                    CollectGem();
                }
                else
                {
                    Debug.Log("You must trade with the NPC before collecting gems.");
                }
            }
            else
            {
                Debug.Log("Player reference is null!");
            }
        }
    }

    private void CollectGem()
    {
        collectedGems++; // Increment collected gems
        Debug.Log("Gem Collected! Total Collected: " + collectedGems);

        // play a sfx here

        // update the UI
        GameUI.instance.UpdateGemsText();

        // destroy the gem after collection
        Destroy(gameObject);

        // check for the win condition
        if (collectedGems >= totalGems)
        {
            GameManager.Instance.WinGame();
        }
    }
}