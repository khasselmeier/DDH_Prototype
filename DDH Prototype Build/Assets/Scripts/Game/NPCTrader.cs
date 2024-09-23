using UnityEngine;

public class NPCTrader : MonoBehaviour
{
    public int upgradeCost = 50; // cost of the weapon upgrade
    public int damageIncrease = 5; // amount to increase weapon damage

    private PlayerBehavior player; // ref to the player
    private bool isPlayerInRange = false; // tracks if the player is in range

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerBehavior>();
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null; // clear player reference
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
            player.gold -= upgradeCost; // deduct gold
            player.rocks.damage += damageIncrease; // upgrade weapon
            player.hasTraded = true; // mark as traded

            GameUI.instance.UpdateGoldText(player.gold); // update gold UI

            Debug.Log("Trade successful! Gold deducted: " + upgradeCost);
        }
        else
        {
            Debug.Log("Not enough gold for upgrade.");
        }
    }
}