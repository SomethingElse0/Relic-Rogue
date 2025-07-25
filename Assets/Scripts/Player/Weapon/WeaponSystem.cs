using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    // Start is called before the first frame update
    InputActionAsset actions;
    Vector3 direction;
    public int ammo;//I will need to go back and change this out to get ammo to replenish automatically
    public int maxAmmo;
    GameObject bullet;
    public Transform bulletLocation;
    GameObject dashDestination;
    float timeSinceLastReload;
    float timeOfLastAttack;
    WeaponData GunData;
    private void Awake()
    {
        bullet = transform.GetChild(0).gameObject;
        actions = transform.parent.GetComponent<PlayerMovement>().actions;
        dashDestination = transform.parent.GetChild(transform.GetSiblingIndex() - 2).gameObject;
        bullet.SetActive(false);
    }
    void Update()
    {
        maxAmmo = GunData.maxAmmo;
        /*if (Input.mousePresent) direction = Camera.main.ScreenToWorldPoint(Vector3.Normalize(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)-transform.position));
        else*/ direction = actions.FindActionMap("Movement").FindAction("movement").ReadValue<Vector2>();
        transform.LookAt(direction+transform.position);
    }

    // Update is called once per frame
    public void Attack()
    {
        if (ammo > 0&&Time.time>timeSinceLastReload + GunData.reloadTime &&Time.time>timeOfLastAttack)
        {
            GameObject newBullet = Instantiate(bullet, transform.position +0.1f*direction.normalized, transform.rotation, transform);
            newBullet.SetActive(true);
            newBullet.transform.parent = bulletLocation;
            newBullet.transform.GetComponent<Bullet>().Bounces(2, direction.normalized, GunData.damage);
            ammo--;
            
            timeOfLastAttack = Time.time + GunData.attackCooldown;
        }
        if (ammo == 0) Reload();
    }
    void Reload()
    {
        timeSinceLastReload = Time.time;
        ammo = GunData.maxAmmo;
    }
    void ChangeWeapon(WeaponData newWeapon)
    {
        GunData = newWeapon;
        Reload();
    }
    void PickUpBullet()
    {
        if (ammo < GunData.maxAmmo) ammo++;
    }
}
