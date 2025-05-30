using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WeaponData : ScriptableObject
{
    public int maxAmmo;
    public float attackCooldown;
    public float reloadTime;
    public int damage;
    public int bulletBounces;
    public bool bulletsCollectable;
    public bool isMelee;
    public Sprite gunSprite;
}
