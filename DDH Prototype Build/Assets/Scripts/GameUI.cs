using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI goldText;
    //public Slider healthBar;

    private PlayerBehavior player;

    //instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        /*healthBar.maxValue = player.maxHp;
        healthBar.value = player.curHp;*/

        UpdateAmmoText();
        //UpdateGoldText();
    }

    /*public void UpdateGoldText(int gold)
    {
        goldText.text = "<b>Gold:</b> " + gold;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = player.curHp;
    }*/

    public void UpdateAmmoText()
    {
        ammoText.text = player.rocks.curAmmo + " / " + player.rocks.maxAmmo;
    }
}