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
    public GameObject bullet;
    public GameObject bulletPrefab;
    public float fireRate = 5.0f;
    public float shootTimer;
    public float shootTime = 5.0f;

    void Start()
    {
        player = GameObject.Find("Character");
        numPivots = pivots.Length;
        mRigidBody = GetComponent<Rigidbody>();
        flyingCounter = flyDuration;
        delayedLook = lookTime;
        shootTimer = shootTime;
        //bullet = GameObject.Find("EnemyBulletPrefab");
        //bulletPrefab = (GameObject)Resources.Load("Assets/prefabs/EnemyBullet.prefab", typeof(GameObject));
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

    void shoot()
    {
        //checkIfBulletExists();
        if (bullet != null)
        {
            Destroy(bullet);
        }
        if (bullet == null)
        {
            Vector3 startingPos = transform.position;
            //startingPos.y *= -20;
            startingPos += transform.forward * -3.0f;
            startingPos += transform.up * 1.0f;
            bullet = GameObject.Instantiate(bulletPrefab, startingPos, Quaternion.identity);
            //bullet.GetComponentInChildren<Transform>().Scale(20.0f, 20.0f, 20.0f);
            //bullet.GetComponentInChildren<Transform>().Rotate(0.0f, 180.0f, 0.0f);
            //bullet.GetComponent<Transform>().Translate(transform.position - transform.forward * 20);
            //Vector3 bulletForce = -transform.backward;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * -500f);
        }
        //GameObject bullet;
        //bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        //bullet.GetComponentInChildren<RigidBody>().MoveRotation(0.0f, 180.0f, 0.0f);
        //bullet.GetComponent<Transform>.MovePosition(transform.position + transform.back * 2);
        //bullet.GetComponent<Rigidbody>.AddForce(transform.forward);
        //bullet.GetComponent<enemyBullet>().init(bulletLifetime);
    }

    void fire()
    {
        if (shootTimer <= 0.0f)
        {
            shoot();
            shootTimer = shootTime;
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
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
