using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
        public GameSystem sistema;
        public string objetivo;
    // Start is called before the first frame update
    void Start()
    {
        sistema.MostrarObjetivo();
        sistema.UpdateObjetivo(objetivo); 
    }


}
