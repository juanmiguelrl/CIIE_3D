using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscenario : MonoBehaviour
{
    public string nombre_escena;
    // Start is called before the first frame update
    public void CargandoEscenario(string nombre_escena) {
        SceneManager.LoadScene(nombre_escena);
    }
}
