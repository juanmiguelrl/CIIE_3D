using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public bool rotates = false;
    public bool moves = false;
    public bool oscilates = false;
    public Vector3[] pivots;
    public float movementSpeed;
    public float rotateSpeed;
    public float oscilateSpeed;
    public float maxOscilation;
    public int nextPivot;
    public int numPivots;
    public enum oscilatableAxis { x, z};
    public oscilatableAxis oscilateAxis;
    private bool oscilatingUp = true;
    private float currentOscilation = 0.0f;
    public GameObject player;
    public bool playerOn=false;

    public GameObject plataforma;

    void Start()
    {
        numPivots = pivots.Length;
    }

    int circArray(int i)
    {
        return (i % numPivots);
    }

    void updatePivot()
    {
        if (Vector3.Distance(transform.position, pivots[nextPivot]) < 0.5)
        {
            nextPivot = circArray(nextPivot + 1);
        }
    }

    void move()
    {
        updatePivot();
        Vector3 nextPosition = Vector3.MoveTowards(transform.position,pivots[nextPivot], Time.deltaTime*movementSpeed);
        if (playerOn)
        {
            //player.GetComponent<RigidBody>.MovePosition(nextPosition);
            player.transform.position = nextPosition;
        }
        transform.position = nextPosition;
    }

    void rotate()
    {
        transform.Rotate(0.0f, Time.deltaTime * rotateSpeed, 0.0f);
    }

    void oscilate()
    {
        Debug.Log(transform.rotation.eulerAngles);
        if (oscilateAxis == oscilatableAxis.x)
        {
            if (oscilatingUp)
            {
                if (transform.rotation.eulerAngles.x < maxOscilation || transform.rotation.eulerAngles.x >= 360.0f - maxOscilation -2.0f)
                {
                    transform.Rotate(oscilateSpeed * Time.deltaTime, 0.0f, 0.0f);
                }
                else
                {
                    oscilatingUp = false;
                }
            } else
            {
                if (transform.rotation.eulerAngles.x > 360.0f-maxOscilation || transform.rotation.eulerAngles.x <= (maxOscilation+2.0f))
                {
                    transform.Rotate(-oscilateSpeed * Time.deltaTime, 0.0f, 0.0f);
                }
                else
                {
                    oscilatingUp = true;
                }
            }
        } else if (oscilateAxis == oscilatableAxis.z)
        {
            if (oscilatingUp)
            {
                if (transform.rotation.eulerAngles.z < maxOscilation || transform.rotation.eulerAngles.z >= 360.0f - maxOscilation - 2.0f)
                {
                    transform.Rotate(0.0f, 0.0f, oscilateSpeed * Time.deltaTime);
                }
                else
                {
                    oscilatingUp = false;
                }
            }
            else
            {
                if (transform.rotation.eulerAngles.z > 360.0f-maxOscilation || transform.rotation.eulerAngles.z <= (maxOscilation + 2.0f))
                {
                    transform.Rotate(0.0f, 0.0f, -oscilateSpeed * Time.deltaTime);
                }
                else
                {
                    oscilatingUp = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moves)
        {
            move();
        }
        if (rotates){
            rotate();
        }
        if (oscilates){
            oscilate();
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Vector3 scale = player.transform.localScale;
            Debug.Log("Player entered");
            player.transform.parent = transform;
            //player.transform.localScale = UnityEngine.Vector3.Scale(1,1,1);
            //player.transform.localScale = scale;
            playerOn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Player exited");
            player.transform.parent = null;
            playerOn = false;
        }
    }
}
