using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    // Start is called before the first frame update
    InputActionAsset actions;
    Vector3 direction;
    float oldDirection=1;
    int ammo;//I will need to go back and change this out to get ammo to replenish automatically
    GameObject bullet;
    float timeSinceLastReload;
    float timeOfLastAttack;
    WeaponData GunData;

    void Update()
    {
        if (Input.mousePresent) direction = Camera.main.ScreenToWorldPoint(Vector3.Normalize(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)));
        else direction = actions.FindActionMap("Movement").FindAction("movement").ReadValue<Vector2>();
        if (direction.x * oldDirection < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); 
            oldDirection = direction.x;
        }
        transform.LookAt(direction);
    }

    // Update is called once per frame
    public void Attack()
    {
        if (ammo > 0&&Time.time>timeSinceLastReload + GunData.reloadTime &&Time.time>timeOfLastAttack)
        {
            Instantiate(bullet, transform.position + direction, transform.rotation);
            ammo--;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*5);
            timeOfLastAttack = Time.time + GunData.attackCooldown;
        }
    }
    void Reload()
    {
        timeSinceLastReload = Time.time;
        ammo = GunData.maxAmmo;
    }
    void ChangeWeapon(ScriptableObject newWeapon)
    {
        // set variables to newWeapon Variables including: Ammo, bullet, reload time, attack cooldown, max ammo, gun model
        Reload();
    }
}
