using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip hit;
    public InputActionAsset actions;
    InputAction dash;
    InputAction attack;
    InputAction movement;
    InputAction move;
    InputAction interact;
    InputAction changeWeapon;
    InputAction pause;
    public GameObject weapon;
    GameObject dashDestination;
    public Deck deck;
    public Deck originalDeck;
    public bool dailyReward;
    public List<ScriptableObject> weapons = new List<ScriptableObject>();
    Ray ray;
    float playerSpeed = 5f;
    float dashCoolldown = 1.5f;
    float lastDashTime = -10;
    public float hp = 20;
    public int maxHP = 20;
    Vector3 velocity=new Vector3 (0,0,0);
    Vector3 savedVelocity=new Vector3 (0,1,0);
    Vector3 dashDirection;
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
        dashDestination = transform.GetChild(0).gameObject;
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
        rb = GetComponent<Rigidbody>();
        ray.origin = transform.position;
        ray.direction = dashDestination.transform.localPosition;
        weapon=transform.GetChild(transform.childCount - 1).gameObject;
        weapon.SendMessage("ChangeWeapon", weapons[0], SendMessageOptions.DontRequireReceiver);
        dailyReward = System.DateTime.Today != playerData.LastPlayed;
        if (weapon.activeInHierarchy) 
        {
            deck.player = gameObject;
            deck.weapon = weapon;
        }
        playerData.coins = originalDeck.coin + deck.coin + 0.2f * (deck.scrap + originalDeck.scrap);
        playerData.keys += Mathf.RoundToInt(0.1f * (deck.scrap+originalDeck.scrap) - 0.5f);
        deck.coin = 0;
        deck.rations = 0;
        deck.fuel = 85;
        deck.scrap = 0;
        originalDeck.coin = 0;
        originalDeck.rations = 0;
        originalDeck.fuel = 85;
        originalDeck.scrap = 0;
    }

    public void OnEnable() => actions.Enable();
    private void OnDisable() => actions.Disable();
    public void OnHit(float damage)
    {
        
        if (deck.hpTemp > damage) deck.hpTemp -= damage;
        else
        {
            hp -= damage-deck.hpTemp;
            deck.hpTemp = 0;
        }
        sfx.PlayOneShot(hit);
    }
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
        if(other.gameObject.tag=="Interactable")
        {
            interactableObject = other.gameObject; 
        }
        if (other.gameObject.tag == "ammo") OnHit(other.gameObject.GetComponent<Bullet>().damage);
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactableObject == other.gameObject) interactableObject = null;
    }
    void Update()
    {
        
        if (deck.fuel > 100) deck.fuel = 100;
        if (weaponEnabled != weapon.activeInHierarchy) weapon.SetActive(weaponEnabled);
        if (hp > 0)
        {
             Vector2 movementValue = playerData.playerSpeed * movement.ReadValue<Vector2>();
            rb.velocity = new Vector3(movementValue.x, movementValue.y, 0);

            if (velocity.magnitude != 0 && velocity / velocity.magnitude != savedVelocity) savedVelocity = velocity / velocity.magnitude;
            
        }
        else
        {
            weaponEnabled = false;
            deck.coin = 0;
            deck.rations = 0;
            deck.fuel = 0;
            deck.scrap = 0;
            OnDeath();
        }
        if (closeEnemies > 0 && battle.volume < 0.7) battle.volume += 0.03f;
        else if (closeEnemies == 0 && battle.volume > 0) battle.volume -= 0.001f;
        else closeEnemies = 0;
        if (hp < maxHP && deck.rations > 0)
        {
            hp++;
            deck.rations--;
        }
    }
    void OnInteract(InputAction.CallbackContext context)
    {
        if (interactableObject!=null)interactableObject.SendMessage("Interact", transform,SendMessageOptions.DontRequireReceiver);
    }
    void OnDash(InputAction.CallbackContext context)
    {
        if (deck.fuel > 0)
        {
            if (lastDashTime + dashCoolldown < Time.fixedTime)
            {
                lastDashTime = Time.fixedTime;
                if (velocity.magnitude > 0) dashDirection = velocity * playerSpeed * 3 / velocity.magnitude;
                else dashDirection = savedVelocity * playerSpeed * 3;
                dashDestination.SendMessage("OnDash",SendMessageOptions.DontRequireReceiver);
                Vector3 correction = new Vector3(-1, 0);
                deck.fuel -= 5;
            }
        }
    }
    void OnMove(InputAction.CallbackContext context)
    {
        velocity = new Vector3(movement.ReadValue<Vector2>().x, movement.ReadValue<Vector2>().y,0);
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        if (weaponEnabled) weapon.SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
    }
    void OnPause(InputAction.CallbackContext context)
    {   
        OnDisable();
        pauseMenu.gameObject.SetActive(true);
    }
    public void Pickup(string item)
    {
        print("pickup: "+item);
        if (item == "coin") deck.coin++;
        else if (item == "scrap") deck.scrap+=Random.Range(1,3);
        else if (item == "fuel") deck.fuel+=Random.Range(6,15);
        else if (item == "ration") deck.rations++;
        else if (item == "levelkey") levelKey=true;
        else if (item == "coin(clone)") deck.coin++;
        else if (item == "scrap(clone)") deck.scrap += Random.Range(1, 3);
        else if (item == "fuel(clone)") deck.fuel += Random.Range(6, 15);
        else if (item == "ration(clone)") deck.rations++;
        else if (item == "levelkey(clone)") levelKey = true;
    }
    public void OpenDoor()
    {
        levelKey = false;
    }
    void ChangeWeapon(InputAction.CallbackContext callbackContext)
    {
        weapons.Add(weapons[0]);
        weapon.SendMessage("ChangeWeapon", weapons[1], SendMessageOptions.DontRequireReceiver);
        weapons.RemoveAt(0);
    }
    void OnDeath()
    {
        actions.Disable();
        SceneManager.LoadScene(1);
    }
}
