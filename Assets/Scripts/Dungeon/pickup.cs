using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    // Start is called before the first frame update
    float startTime;
    public GenerateScrap generator;
    public  PlayerMovement player;
    public int awakeTime;
    private void Awake()
    {
        Destroy(gameObject, 60);
        awakeTime = Time.frameCount;
        if(name.ToLower()!="levelkey")transform.rotation=generator.transform.GetChild(0).localRotation;
        else transform.rotation = generator.transform.GetChild(4).localRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.3f);
    }
    private void Update()
    {
        if (transform.parent.childCount - 20 > transform.GetSiblingIndex())Destroy(gameObject,5);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject== player.gameObject)
        {
            player.Pickup(name.ToLower());
            Destroy(gameObject);
        }
        /*
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            if (gameObject.name == "Scrap") player.scrap += Random.Range(2, 7);
            else if (gameObject.name == "Coin") player.deck.coin += Random.Range(3, 9);
            else if (gameObject.name == "Fuel") player.deck.fuel += Random.Range(6, 17);
            else if (gameObject.name == "Ration") player.rations += Random.Range(1, 4);
            else if (gameObject.name == "LevelKey") player.levelKey = true;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Wall")
        {
            print("i'm gonna die");/////////////////////////////
            generator.GenerateSpecific(gameObject.name.ToLower());
            Destroy(gameObject);
                
        }
        */
    }
    // Update is called once per frame
}
