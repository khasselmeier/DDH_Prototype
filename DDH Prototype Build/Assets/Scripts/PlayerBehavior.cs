using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer mr;
    public PlayerRocks rocks;
    //public bool isGrounded = true;

    //public int gold;

    public void Initialize()
    {
        GameUI.instance.Initialize();
        rig.isKinematic = true;
    }

    
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();

        if (Input.GetMouseButtonDown(0))
            rocks.TryShoot();
    }

    void Move()
    {
        //get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        //set that as our velocity
        rig.velocity = dir;
    }

    void TryJump()
    {
        Debug.Log("Jumping");

        //create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        //shoot the raycast
        if (Physics.Raycast(ray, 6.0f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /*void GiveGold(int goldToGive)
    {
        gold += goldToGive;

        //update the ui
        GameUI.instance.UpdateGoldText(gold);
    }*/

    public void ChangeAmmo(int amount)
    {
        // Method is called to change ammo
        rocks.curAmmo += amount;

        // Ensure ammo doesn't exceed max
        rocks.curAmmo = Mathf.Clamp(rocks.curAmmo, 0, rocks.maxAmmo);

        // Notify the UI to update
        GameUI.instance.UpdateAmmoText();
    }
}