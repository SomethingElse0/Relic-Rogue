using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponMode;//this is for separating different weapon types is readable
    public int ammo;//current ammount of ammo
    public int maxAmmo;//max ammount of ammo 
    public float attackCooldown;//time between attacks in seconds
    public float reloadTime;//the ammount of time to reload
    public int damage;//base ammount of damage dealt
    public int bulletBounces;//the number of times the bullet will bounce before being destroyed
    public bool bulletsCollectable;//determines if the player cam reload by picking up the bullets
    public bool isMelee;//this is not used yet, but eventually I would like to have a melee weapon
    public Mesh gunSprite;//this is where the model is stored
    public string bulletType;//this is to determine which (if any) special properties the bullets have
}
