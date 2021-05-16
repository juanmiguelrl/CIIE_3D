using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

public GameObject objecto;
public float comienzo;
public float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawnear",comienzo,tiempo);
        
    }

    public void Spawnear(){
        Instantiate(objecto,transform.position, transform.rotation);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
