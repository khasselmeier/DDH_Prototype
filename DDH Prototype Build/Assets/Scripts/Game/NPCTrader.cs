using UnityEngine;
using TMPro;

public class NPCTrader : MonoBehaviour
{
    public int upgradeCost = 50;
    public int damageIncrease = 5;
    public TextMeshProUGUI tradePromptText; // Reference to the UI Text that will display the prompt

    private PlayerBehavior player; // ref to the player
    private bool isPlayerInRange = false; // tracks if the player is in range to trade
    private bool hasTraded = false;

    private void Start()
    {
        // trade prompt is hidden at the start
        tradePromptText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTraded && other.CompareTag("Player")) // only show prompt if the player hasn't traded yet
        {
            player = other.GetComponent<PlayerBehavior>();
            isPlayerInRange = true;

            // show trade prompt UI when in range
            tradePromptText.gameObject.SetActive(true);
            tradePromptText.text = "Trade 50 gold for a pickaxe";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null; // clear player ref

            // hide the trade prompt UI when the player leaves the range
            if (!hasTraded)
            {
                tradePromptText.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && player != null && Input.GetKeyDown(KeyCode.F))
        {
            TradeWithPlayer(player);
        }
    }

    private void TradeWithPlayer(PlayerBehavior player)
    {
        if (player.gold >= upgradeCost)
        {
            player.gold -= upgradeCost;
            player.rocks.damage += damageIncrease;
            player.hasTraded = true;

            if (player.pickaxe != null)
            {
                player.pickaxe.GetComponent<MeshRenderer>().enabled = true;
                Debug.Log("Pickaxe is now visible");
            }

            GameUI.instance.UpdateGoldText(player.gold); // update gold UI

            Debug.Log("Trade successful! Gold deducted: " + upgradeCost);

            tradePromptText.gameObject.SetActive(false);
            hasTraded = true;
        }
        else
        {
            Debug.Log("Not enough gold for upgrade.");
        }
    }
}