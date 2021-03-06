using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class robotEnemy : MonoBehaviour
{
    public Rigidbody mRigidBody;
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
    public const float backSpeed = 1.0f;
    public int life = 3;
    public Transform jugador;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        //jugador = transform.Find("Character");
        //jugador = GameObject.Find("Character").transform;
        jugador = GameObject.Find("Character").transform;
    }

    float checkPlayerPosition()
    {
        /*Vector3 player = transform.Find("Character").position;
        //jugador = GameObject.Find("Character");
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(player,currentPosition);
        if (distance < 25)
        {
            this.playerDetected = true;
        } else
        {
            this.playerDetected = false;
        }
        return distance;*/
        //Vector3 player = transform.Find("Character").position;
        Vector3 player = GameObject.Find("Character").transform.position;
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(player, currentPosition);
        if (distance < 25)
        {
            this.playerDetected = true;
        }
        else
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

    Vector3 lookAtEnemy()
    {
        Vector3 player = GameObject.Find("Character").transform.position;
        Vector3 lookVector = player - transform.position;
        lookVector.y = transform.position.y;
        //lookVector.x = transform.position.x;
        //lookVector.z = transform.position.z;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        transform.Rotate(0.0f,180.0f,0.0f);
        return player;
    }

    void getAwayFromEnemy(Vector3 playerPosition)
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * backSpeed);
        //Vector3 movedPos = transform.position;
        //Vector3 direction = (target.transform.position - transform.position).normalized;
        //mRigidBody.MovePosition(playerPosition);
    }

    void fire()
    {

    }

    void goToEnemy(Vector3 playerPosition)
    {
        //transform.Translate(Vector3.back * Time.deltaTime * backSpeed);
        //transform.Translate(Vector3.forward * Time.deltaTime * backSpeed);
        //Vector3 movedPos = transform.position;
        //Vector3 direction = (target.transform.position - transform.position).normalized;
        //mRigidBody.MovePosition(playerPosition);
        if (Vector3.Distance(transform.position, playerPosition) > 8.0)
        {
            transform.LookAt(jugador);
            mRigidBody.AddRelativeForce(Vector3.forward * 0.05f, ForceMode.VelocityChange);
        }
    }

    void fightEnemy(float distance)
    {
        Vector3 playerPosition = lookAtEnemy();
        if (distance < 5.0f)
        {
            getAwayFromEnemy(playerPosition);
        }
        else if (distance > 8.0f)
        {
            goToEnemy(playerPosition);
            fire();
        }
    }

    void idling()
    {
        Vector3 currentPosition = transform.position;
        if (exploringFront)
        {
            transform.Translate(Vector3.back * Time.deltaTime * exploringSpeed);
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
            transform.Translate(Vector3.back * Time.deltaTime * exploringSpeed);
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
        if (!playerDetected)
        {
            idling();
        }
        else
        {
            fightEnemy(distance);
        }
        //Debug.Log("Player detected is: " + playerDetected + ". Distance is: "+distance);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("This object has collided with me: "+other.collider.gameObject.name);
    }
}
