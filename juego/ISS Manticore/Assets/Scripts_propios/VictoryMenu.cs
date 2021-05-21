using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public string nombre_escena;
    public string nombre_objeto_a_colisionar;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == nombre_objeto_a_colisionar)
        {
            SceneManager.LoadScene(nombre_escena);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
