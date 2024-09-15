using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //instance 
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    /*
    public PlayerBehavior GetPlayer(GameObject playerObject)
    {
        foreach (PlayerBehavior player in players)
        {
            if (player != null && player.gameObject == playerObject)
                return player;
        }

        return null;
    }*/
}
