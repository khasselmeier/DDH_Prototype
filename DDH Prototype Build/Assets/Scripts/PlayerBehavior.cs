using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 20f;

    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    //public bool demoKinematicMovement = false;
    public bool isGrounded = true;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    //private GameBehavior _gameManager;

    public int gold;

    private bool doJump = false;
    private bool doShoot = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        /*if (demoKinematicMovement)
        {
            MoveKinematically();
        }*/
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }
        if (Input.GetMouseButtonDown(0)) //player will throw rocks
        {
            doShoot = true;
        }

        /*if (demoKinematicMovement)
        {
            return;
        }*/

        if (doJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            doJump = false;
        }
    }

    private void OnCollision(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
        /*if (collision.gameObject.name == "enemy")
        {
            _gameManager.HP -= 1;
        }*/
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
        if (doShoot)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            doShoot = false;
        }
    }

    /*void MoveKinematically()
    {
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    }*/

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
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
    /*
    void GiveGold(int goldToGive)
    {
        gold += goldToGive;

        //update the ui
        GameUI.instance.UpdateGoldText(gold);
    }*/
}