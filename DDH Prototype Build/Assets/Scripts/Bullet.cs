using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rig;

    //public float onscreenDelay = 3f;

    public void Initialize()
    {
        Destroy(gameObject, 3.0f);
    }

    /*void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }*/
}