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
    public bool isGrounded = true;

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
        if (Physics.Raycast(ray, 100.0f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Gem1":
                Debug.Log("Gem1 Found");
                Destroy(GameObject.FindWithTag("Gem1"));
                break;
            case "Gem2":
                Debug.Log("Gem2 Found");
                Destroy(GameObject.FindWithTag("Gem2"));
                break;
            case "Gem3":
                Debug.Log("Gem3 Found");
                Destroy(GameObject.FindWithTag("Gem3"));
                break;
            case "Gem4":
                Debug.Log("Gem4 Found");
                Destroy(GameObject.FindWithTag("Gem4"));
                break;
            case "Gem5":
                Debug.Log("Gem5 Found");
                Destroy(GameObject.FindWithTag("Gem5"));
                break;
            case "Gem6":
                Debug.Log("Gem6 Found");
                Destroy(GameObject.FindWithTag("Gem6"));
                break;
        }
    }

    /*void GiveGold(int goldToGive)
    {
        gold += goldToGive;

        //update the ui
        GameUI.instance.UpdateGoldText(gold);
    }*/
}