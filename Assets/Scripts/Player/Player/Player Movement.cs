using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //   AUDIO
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip hit;
    //   INPUT ACTIONS
    public InputActionAsset actions;
    InputAction dash;
    InputAction attack;
    InputAction movement;
    InputAction move;
    InputAction interact;
    InputAction changeWeapon;
    InputAction pause;
    //   GUN & GUN STATS
    public GameObject weapon;
    public List<ScriptableObject> weapons = new List<ScriptableObject>();
    
    public Deck deck;
    public Deck originalDeck;
    //   DASH
    GameObject dashDestination;
    Vector3 dashDirection;
    public bool dailyReward;
    
    float playerSpeed = 5f;
    float dashCoolldown = 1.5f;
    float lastDashTime = -10;
    public float hp = 20;
    public int maxHP = 20;
    Vector3 velocity=new Vector3 (0,0,0);
    Vector3 savedVelocity=new Vector3 (0,1,0);
    
    Rigidbody rb;
    GameObject interactableObject;
    public PlayerData playerData;
    public bool levelKey=false;
    public float closeEnemies;
    public AudioSource battle;
    public AudioSource sfx;
    public Transform pauseMenu;
    public bool weaponEnabled;
    void Awake()
    {
        //assigning input actions
        dash = actions.FindActionMap("Movement").FindAction("dash");
        actions.FindActionMap("Movement").FindAction("dash").performed += OnDash;
        interact = actions.FindActionMap("Movement").FindAction("interact");
        actions.FindActionMap("Movement").FindAction("interact").performed += OnInteract;
        movement = actions.FindActionMap("Movement").FindAction("movement");
        attack = actions.FindActionMap("Movement").FindAction("attack");
        actions.FindActionMap("Movement").FindAction("attack").performed += OnAttack;
        changeWeapon = actions.FindActionMap("Movement").FindAction("change");
        actions.FindActionMap("Movement").FindAction("change").performed += ChangeWeapon;
        pause = actions.FindActionMap("Movement").FindAction("Pause");
        actions.FindActionMap("Movement").FindAction("Pause").performed += OnPause;

        // identifying components and child objects
        rb = GetComponent<Rigidbody>();
        dashDestination = transform.GetChild(0).gameObject;
        weapon =transform.GetChild(transform.childCount - 1).gameObject;
        
        //identifying if daily reward has renewed
        dailyReward = System.DateTime.Today != playerData.LastPlayed;

        //setting the weapon preset, and enabling the deck if the gun is enabled
        if (weapon.activeInHierarchy)
        {
            weapon.SendMessage("ChangeWeapon", weapons[0], SendMessageOptions.DontRequireReceiver);//setting the weapon type 
            deck.player = gameObject;
            deck.weapon = weapon;
        }
        //dealing with coins gained etc. after the run is over
        else
        {
            playerData.coins += originalDeck.coin + deck.coin + 0.2f * (deck.scrap + originalDeck.scrap);
            playerData.keys += Mathf.RoundToInt(0.1f * (deck.scrap + originalDeck.scrap) - 0.5f);
        }
        //reset values
        deck.coin = 0;
        deck.rations = 0;
        deck.fuel = 85;
        deck.scrap = 0;
        originalDeck.coin = 0;
        originalDeck.rations = 0;
        originalDeck.fuel = 85;
        originalDeck.scrap = 0;
    }
    void Update()
    {

        
        if (weaponEnabled != weapon.activeInHierarchy) weapon.SetActive(weaponEnabled);
        if (hp > 0)
        {
             Vector2 movementValue = playerData.playerSpeed * movement.ReadValue<Vector2>();
            rb.linearVelocity = new Vector3(movementValue.x, movementValue.y, 0);

            if (velocity.magnitude != 0 && velocity / velocity.magnitude != savedVelocity) savedVelocity = velocity / velocity.magnitude;
            
        }
        else OnDeath();
        if (closeEnemies > 0 && battle.volume < 0.7) battle.volume += 0.03f;
        else if (closeEnemies == 0 && battle.volume > 0) battle.volume -= 0.001f;
        else closeEnemies = 0;
        if (hp < maxHP && deck.rations > 3)
        {
            hp++;
            deck.rations-=3;
        }
    }
    public void OnEnable() => actions.Enable();//enables inputactions
    public void OnDisable() => actions.Disable();//disables inputactions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactableObject = other.gameObject;
        }
        else if (other.gameObject.tag == "ammo") weapon.SendMessage("PickUpBullet", SendMessageOptions.DontRequireReceiver);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Interactable") interactableObject = other.gameObject;//ensuring pickups are collected
        else if (other.gameObject.tag == "ammo") OnHit(other.gameObject.GetComponent<Bullet>().damage);
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactableObject == other.gameObject) interactableObject = gameObject;//setting a default that will not throw errors
    }
    void OnInteract(InputAction.CallbackContext context) => interactableObject.SendMessage("Interact", transform,SendMessageOptions.DontRequireReceiver);//keeping the interaction functions in separate locations so that it can be expanded on without causing issues
    void OnDash(InputAction.CallbackContext context)
    {
        if (deck.fuel > 4)//checking if there is enough fuel to dash
        {
            if (lastDashTime + dashCoolldown < Time.fixedTime)
            {
                lastDashTime = Time.fixedTime;
                if (velocity.magnitude > 0) dashDirection = velocity * playerSpeed * 3 / velocity.magnitude;
                else dashDirection = savedVelocity * playerSpeed * 3;
                dashDestination.GetComponent<Dash_Destination>().OnDash();
                Vector3 correction = new Vector3(-1, 0);
                deck.fuel -= 5;
            }
        }
        
    }
    void OnAttack(InputAction.CallbackContext context) => weapon.GetComponent<WeaponSystem>().Attack();//keeping the input actions in one location for easier trooubleshooting
    void ChangeWeapon(InputAction.CallbackContext callbackContext)//changing weapon
    {
        weapons.Add(weapons[0]);
        weapon.SendMessage("ChangeWeapon", weapons[1], SendMessageOptions.DontRequireReceiver);
        weapons.RemoveAt(0);
    }
    void OnPause(InputAction.CallbackContext context)
    {   
        OnDisable();
        pauseMenu.gameObject.SetActive(true);
    }
    public void OnHit(float damage)//controls recieving damage
    {

        if (deck.hpTemp > damage) deck.hpTemp -= damage;//checking if temp hp will block the damage
        else
        {
            hp -= damage - deck.hpTemp;//reducing the damage taken by the temp hp
            deck.hpTemp = 0;
        }
        sfx.PlayOneShot(hit);//play hurt sound effect
    }
    public void Pickup(string item)
    {
        print("pickup: "+item);
        if (item.Contains("coin")) deck.coin++;
        else if (item.Contains("scrap")) deck.scrap += Random.Range(1, 3);
        else if (item.Contains("fuel"))
        {
            if (100 - deck.fuel > 15) deck.fuel += Random.Range(6, 15);
            else deck.fuel += Random.Range(0, 101 - deck.fuel);
        }
        else if (item.Contains("ration")) deck.rations++;
        else if (item.Contains("levelkey")) levelKey = true;

    }
    public void OpenDoor()=>levelKey = false;//this is to prevent the player from speeding to the end of the second level


    void OnDeath()
    {
        actions.Disable();
        weaponEnabled = false;
        //prevent pickups from that run from being collected, so there's a punishment for dying
        foreach (WeaponData weapon in weapons) weapon.ammo = weapon.maxAmmo;
        deck.coin = 0;
        deck.rations = 0;
        deck.fuel = 85;
        deck.scrap = Mathf.RoundToInt(0.1f*deck.scrap);
        SceneManager.LoadScene(1);
        
    }
}

