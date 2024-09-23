using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI totalGemsText;
    public TextMeshProUGUI healthText;

    private PlayerBehavior player;

    //instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerBehavior>(); // finds the player in the scene
        if (player != null)
        {
            Initialize();
        }
        else
        {
            Debug.LogError("PlayerBehavior not found in the scene.");
        }
    }

    public void Initialize()
    {
        UpdateAmmoText();
        UpdateGemsText();
        UpdateGoldText(player.gold);
        UpdateTotalGemsText(); // update "total quota to win" UI on initialization
        UpdateHealthText(player.currentHealth, player.maxHealth);

    }

    public void UpdateTotalGemsText()
    {
        totalGemsText.text = "Quota to Win: " + GemPickup.totalGems;
    }

    public void UpdateAmmoText()
    {
        if (player != null && player.rocks != null)
        {
            //Debug.Log("Update Ammo UI: " + player.rocks.curAmmo + " / " + player.rocks.maxAmmo);
            ammoText.text = "Rocks: " + player.rocks.curAmmo + " / " + player.rocks.maxAmmo;
        }
        else
        {
            Debug.LogError("Player or player's rocks is not initialized.");
        }
    }

    public void UpdateGemsText()
    {
        gemsText.text = "Quota Collected: " + GemPickup.collectedGems;
    }

    public void UpdateGoldText(int goldAmount)
    {
        goldText.text = "Gold: " + goldAmount;
    }

    public void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = "Health: " + currentHealth + " / " + maxHealth;
    }
}