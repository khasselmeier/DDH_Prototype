/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Gold,
    Ammo
}

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pickup triggered.");

        if (other.CompareTag("Player"))
        {
            PlayerBehavior player = GameManager.instance.GetPlayer(other.gameObject);

            if (type == PickupType.Gold)
                GiveGold(player, value);
            else if (type == PickupType.Ammo)
                GiveAmmo(player, value);

            // destroy the object
            DestroyPickup();
        }
    }

    public void DestroyPickup()
    {
        Destroy(gameObject);
    }
}*/