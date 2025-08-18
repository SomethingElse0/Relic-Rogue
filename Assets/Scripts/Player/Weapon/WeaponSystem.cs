using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    // Start is called before the first frame update
    InputActionAsset actions;
    InputAction aim;
    Vector3 direction;
    public int ammo;//I will need to go back and change this out to get ammo to replenish automatically
    public int maxAmmo;
    GameObject bullet;
    public Transform bulletLocation;
    GameObject dashDestination;
    float timeSinceLastReload;
    float timeOfLastAttack;
    PlayerMovement player;
    public WeaponData GunData;
    Deck deck;
    private void Awake()
    {
        
        bullet = transform.GetChild(0).gameObject;
        player = transform.parent.GetComponent<PlayerMovement>();
        deck = player.deck;
        actions = player.actions;
        dashDestination = transform.parent.GetChild(transform.GetSiblingIndex() - 2).gameObject;
        bullet.SetActive(false);
        aim = actions.FindActionMap("Movement").FindAction("Aim");
    }
    void Update()
    {
        maxAmmo = GunData.maxAmmo;
        if (aim.ReadValue<Vector2>().magnitude>0) direction = aim.ReadValue<Vector2>();
        else direction = actions.FindActionMap("Movement").FindAction("movement").ReadValue<Vector2>();
        transform.LookAt(direction+transform.position);
    }

    // Update is called once per frame
    public void Attack()
    {
        if (ammo > 0&&Time.time>timeSinceLastReload + GunData.reloadTime &&Time.time>timeOfLastAttack)
        {
            GameObject newBullet = Instantiate(bullet, transform.position +0.1f*direction.normalized, bullet.transform.rotation,transform);
            newBullet.SetActive(true);
            newBullet.transform.parent = bulletLocation;
            newBullet.GetComponent<Bullet>().damage = GunData.damage+deck.damageModifier;
            newBullet.GetComponent<Bullet>().deck = deck;
            newBullet.GetComponent<Bullet>().Bounces(GunData.bulletBounces, 7, GunData.damage);
            ammo--;
            int i = Random.Range(1, 3);
            if (i == 1) player.sfx.PlayOneShot(player.attack1);
            else if (i == 2) player.sfx.PlayOneShot(player.attack2);
            else player.sfx.PlayOneShot(player.attack3);
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
