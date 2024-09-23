using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public static int totalGems;
    public static int collectedGems;

    private bool isPlayerInRange = false;
    private PlayerBehavior player; // ref to the player

    [Header("Gem Value Settings")]
    public int minGemValue = 10;
    public int maxGemValue = 100;
    private int gemValue;

    private void Start()
    {
        // total quota random value between 50 and 150
        totalGems = Random.Range(50, 150);
        Debug.Log("Total quota needed to win: " + totalGems);

        // assign a random gem value between minGemValue and maxGemValue
        gemValue = Random.Range(minGemValue, maxGemValue + 1);
        Debug.Log("Gem value assigned: " + gemValue);

        // update UI with the total gems (quota needed to win)
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
            //Debug.Log("Player entered gem pickup range.");

            isPlayerInRange = true;
            player = other.GetComponent<PlayerBehavior>(); // store player ref
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player exited gem pickup range.");

            isPlayerInRange = false;
            player = null; // clear player ref
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // check for left mouse button down (button index 0)
        {
            //Debug.Log("Attempting to collect gem...");
            if (player != null)
            {
                //Debug.Log("Player has traded: " + player.hasTraded);
                if (player.hasTraded) // checks if the player has traded with the NPC
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
            GameManager.instance.WinGame();
        }
    }
}