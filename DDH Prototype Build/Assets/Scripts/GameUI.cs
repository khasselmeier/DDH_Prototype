using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText; // Text for displaying rock amount
    public TextMeshProUGUI goldText; // Text for displaying collected gold
    public TextMeshProUGUI gemsText; // Text for displaying collected gems

    private PlayerBehavior player;

    //instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerBehavior>(); // Find the player in the scene
        if (player == null)
        {
            Debug.LogError("PlayerBehavior not found in the scene.");
        }
        else
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        UpdateAmmoText();
        //UpdateGoldText();
        UpdateGemsText();

    }


    /*public void UpdateGoldText(int gold)
    {
        goldText.text = "<b>Gold:</b> " + gold;
    }*/

    public void UpdateAmmoText()
    {
        if (player != null && player.rocks != null)
        {
            Debug.Log("Update Ammo UI: " + player.rocks.curAmmo + " / " + player.rocks.maxAmmo);
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
}