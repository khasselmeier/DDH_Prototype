/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Gold,
    Health
}

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;

    void OnTriggerEnter (Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerBehavior player = collision.gameObject.GetComponent<PlayerBehavior>();

            if (type == PickupType.Gold)
            {
                PlayerBehavior.player.("GiveGold", player, value);
            }
            else if (type == PickupType.Health)
                player("Heal", player, value);

            Destroy(gameObject);
        }
    }
}*/