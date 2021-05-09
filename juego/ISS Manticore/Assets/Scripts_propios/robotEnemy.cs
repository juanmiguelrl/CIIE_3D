using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotEnemy : MonoBehaviour
{
    public bool playerDetected = false;
    public bool goingUp = true;
    public float flyingCounter = 50.0f;
    public const float flyingSpeed = 1.0f;
    public const float flyingRate = 80.0f;
    public const float exploringLength = 300.0f;
    public bool exploringFront = true;
    public float exploringCounter = 50.0f;
    public const float exploringSpeed = 1.0f;
    public const float exploringRate = 80.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    float checkPlayerPosition()
    {
        //Vector3 player = transform.Find("Character").position;
        Vector3 player = GameObject.Find("Character").transform.position;
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(player,currentPosition);
        if (distance < 25)
        {
            this.playerDetected = true;
        } else
        {
            this.playerDetected = false;
        }
        return distance;
    }

    void baseMovement()
    {
        Vector3 currentPosition = transform.position;
        if (goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * flyingSpeed);
            flyingCounter -= Time.deltaTime * flyingRate;
            if (flyingCounter<0)
            {
                goingUp = false;
                flyingCounter = 50.0f;
            }
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * flyingSpeed);
            flyingCounter -= Time.deltaTime * flyingRate;
            if (flyingCounter < 0)
            {
                goingUp = true;
                flyingCounter = 50.0f;
            }
        }
    }

    void fightEnemy()
    {
        Debug.Log("Soon ill doo something");
    }

    void idling()
    {
        Vector3 currentPosition = transform.position;
        if (exploringFront)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * exploringSpeed);
            exploringCounter -= Time.deltaTime * exploringRate;
            if (exploringCounter < 0)
            {
                transform.Rotate(0.0f, 180.0f, 0.0f);
                exploringFront = false;
                exploringCounter = exploringLength;
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * exploringSpeed);
            exploringCounter -= Time.deltaTime * exploringRate;
            if (exploringCounter < 0)
            {
                //transform.Rotate(0.0f, 180.0f, 0.0f);
                exploringFront = true;
                exploringCounter = exploringLength;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checkPlayerPosition();
        float distance = checkPlayerPosition();
        baseMovement();
        idling();
        if (!playerDetected)
        {
            //idling();
        }
        else
        {
            fightEnemy();
        }
        Debug.Log("Player detected is: " + playerDetected + ". Distance is: "+distance);
    }
}
