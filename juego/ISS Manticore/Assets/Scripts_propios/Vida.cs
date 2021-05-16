using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int numeroVidas;

    void OnTriggerEnter(Collider other)
    {
        Controller c = other.GetComponent<Controller>();

        if (c != null)
        {
            PlayerStats.Instance.Heal(numeroVidas);
        }
    }

}
