using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocks : MonoBehaviour
{
    [Header("Rock Stats")]
    public int damage;
    public int curAmmo;
    public int maxAmmo;
    public float bulletSpeed;
    public float shootRate;

    private float lastShootTime;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;

    private PlayerBehavior player;

    void Awake()
    {
        //get required components
        player = GetComponent<PlayerBehavior>();
    }

    public void TryShoot()
    {
        //can we throw rocks?
        if (curAmmo <= 0 || Time.time - lastShootTime < shootRate)
            return;

        curAmmo--;
        lastShootTime = Time.time;

        //update ammo UI
        GameUI.instance.UpdateAmmoText();

        //spawn the bullet
        SpawnBullet(bulletSpawnPos.position, Camera.main.transform.forward);

        /*GameObject newBullet = Instantiate(bulletSpawnPos, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = this.transform.forward * bulletSpeed;*/
    }

    void SpawnBullet(Vector3 pos, Vector3 dir)
    {
        //spawn and orientate it
        GameObject bulletObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bulletObj.transform.forward = dir;

        //get the bullet script
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();

        //intialize it and set the velocity
        //bulletScript.Initialize(damage, player.id, player.photonView.IsMine);
        bulletScript.Initialize();
        //bulletScript.rig.velocity = dir * bulletSpeed;
    }

    /*public void GiveAmmo(int ammoToGive)
    {
        curAmmo = Mathf.Clamp(curAmmo + ammoToGive, 0, maxAmmo);

        //update ammo UI
        GameUI.instance.UpdateAmmoText();
    }*/
}
