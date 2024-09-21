using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public static int totalGems; // Total number of gems needed to win
    public static int collectedGems; // number of gems collected

    private bool isPlayerInRange = false; // tracks if the player is in range
    private PlayerBehavior player; // ref to the player

    [Header("Gem Value Settings")]
    public int minGemValue = 0; // Minimum value for gem pickup
    public int maxGemValue = 70; // Maximum value for gem pickup
    private int gemValue; // Random value for the gem pickup

    private void Start()
    {
        // total quota random value between 50 and 150
        totalGems = Random.Range(50, 150);
        Debug.Log("Total quota needed to win: " + totalGems);

        // assign a random gem value between minGemValue and maxGemValue
        gemValue = Random.Range(minGemValue, maxGemValue + 1);
        Debug.Log("Gem value assigned: " + gemValue);

        // Update the UI with the total gems
        if (GameUI.instance != null)
        {
            GameUI.instance.UpdateTotalGemsText();
        }
        else
        {
            Debug.LogError("GameUI instance is not initialized.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered gem pickup range.");

            isPlayerInRange = true; // Player is in range
            player = other.GetComponent<PlayerBehavior>(); // Store the player reference
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited gem pickup range.");

            isPlayerInRange = false; // Player has exited the range
            player = null; // Clear player reference
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // check for left mouse button down (button index 0)
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
        collectedGems += gemValue; // increment collected gems by the random gem value
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