using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody mRigidBody;
    public Vector3[] pivots;
    private int numPivots;
    public int currentPivot;
    public GameObject player;
    public float thressHold = 0.5f;
    public float speed = 0.5f;
    public float flyingSpeed = 1.0f;
    public float flyingRate = 80.0f;
    public float flyingCounter;
    public float flyDuration = 50.0f;
    public bool flyingUp = true;
    public bool playerDetected = false;
    public float detectRadius = 25.0f;
    public bool chasing = false;
    public Vector3 returnPos;
    public float delayedLook;
    public float lookTime = 0.1f;
    public float lookRate = 1.0f;
    public float chaseSpeed = 5.0f;
    public float enemyNearRadius = 5.0f;

    void Start()
    {
        player = GameObject.Find("Character");
        numPivots = pivots.Length;
        mRigidBody = GetComponent<Rigidbody>();
        flyingCounter = flyDuration;
        delayedLook = lookTime;
    }

    int circArray(int i)
    {
        return i % numPivots;
    }

    void updatePivot()
    {
        if (Vector3.Distance(transform.position, pivots[circArray(currentPivot + 1)])<thressHold)
        {
            //Actualizamos el pivot del que proviene
            currentPivot = circArray(currentPivot + 1);
            //Marcamos la posicion a la que debe mirar como lookAt de la posición del siquiente pivot, pero manteniendo y fijo para evitar que se curve el robot
            Vector3 lookPosition = pivots[circArray(currentPivot + 1)];
            lookPosition.y = transform.position.y;
            transform.LookAt(lookPosition);
            transform.Rotate(0, 180.0f, 0);
        }
    }

    void patroll(Vector3 futurePosition)
    {
        updatePivot();
        float step = speed * Time.deltaTime;
        mRigidBody.MovePosition(Vector3.MoveTowards(futurePosition, pivots[circArray(currentPivot + 1)], step));
    }

    Vector3 fly()
    {
        Vector3 futurePosition = transform.position;
        if (flyingUp)
        {
            flyingCounter -= flyingRate * Time.deltaTime;
            futurePosition.y += flyingSpeed * Time.deltaTime;
            mRigidBody.MovePosition(futurePosition);
            if (flyingCounter < 0)
            {
                flyingCounter = flyDuration;
                flyingUp = false;
            }
        }
        else
        {
            flyingCounter -= flyingRate * Time.deltaTime;
            futurePosition.y -= flyingSpeed * Time.deltaTime;
            mRigidBody.MovePosition(futurePosition);
            if (flyingCounter < 0)
            {
                flyingCounter = flyDuration;
                flyingUp = true;
            }
        }
        return futurePosition;
    }

    void checkPlayerDetected()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < detectRadius)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, player.transform.position, out hit))
            {
                if (hit.transform == player.transform)
                {
                    playerDetected = true;
                }
                else
                {
                    playerDetected = false;
                }
            }
            else
            {
                playerDetected = false;
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    void chasePlayer(Vector3 futurePosition)
    {
        if (!chasing)
        {
            chasing = true;
            returnPos = transform.position;
        }
        delayedLook -= Time.deltaTime * lookRate;
        if (delayedLook < 0)
        {
            delayedLook = lookTime;
            transform.LookAt(player.transform.position);
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        else
        {
            if (Vector3.Distance(futurePosition, player.transform.position)>enemyNearRadius)
            {
                float step = chaseSpeed * Time.deltaTime;
                mRigidBody.MovePosition(Vector3.MoveTowards(futurePosition, player.transform.position, step));
            }
        }
        /*transform.LookAt(player.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f*/
        //mRigidBody.MoveRotation()
        //transform.position = Vector3.MoveTowards(futurePosition, player.transform.position, step);
    }

    void fire()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerDetected();
        Vector3 futurePosition = fly();
        //Vector3 futurePosition = transform.position;
        if (!playerDetected)
        {
            patroll(futurePosition);
        }
        else
        {
            chasePlayer(futurePosition);
            fire();
        }
    }
}
