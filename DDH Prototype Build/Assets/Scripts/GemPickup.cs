using UnityEngine;

public class GemPickup : MonoBehaviour
{
    private const int totalGems = 6; // Total number of gems needed to win (constant number)
    public static int collectedGems; // Number of gems collected

    /*private void OnEnable()
    {
        totalGems++; // Increment total gems when a new gem is created
    }

    private void OnDisable()
    {
        totalGems--; // Decrement if the gem is destroyed
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedGems++; // Increment collected gems
            Debug.Log("Gem Collected! Total Collected: " + collectedGems);
            //Play a sound effect here

            // Update the UI
            GameUI.instance.UpdateGemsText();

            // Destroy the gem after collection
            Destroy(gameObject);

            // Check for win condition
            if (collectedGems >= totalGems)
            {
                GameManager.Instance.WinGame();
            }
        }
    }
}
