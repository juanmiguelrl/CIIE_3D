using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair_zone : MonoBehaviour
{

    public float repair_rate;
    public float repair_level = 0;
    public string nombre_objeto_a_colisionar;
    public bool en_la_zona = false;
    public bool repaired = false;
    public string objetivoVolver;
    public string objetivoArreglarMotor;
    // Start is called before the first frame update

    public GameSystem sistema;

    void Start()
    {
        sistema.MostrarReparacion();
        sistema.MostrarObjetivo();
        sistema.UpdateObjetivo(objetivoArreglarMotor);
    }

    // Update is called once per frame
    void Update()
    {
        if (en_la_zona && (repair_level < 100)) {
            repair_level += Time.deltaTime * repair_rate;
        } 
        if(repair_level >= 100) {
            repaired = true;
            sistema.UpdateObjetivo(objetivoVolver);
            sistema.SiguienteNivel();
        }
        if (sistema != null) {
            sistema.UpdateReparacion(repair_level);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == nombre_objeto_a_colisionar) {
            en_la_zona = true;
        }
    }
    private void OnTriggerExit(Collider other){ // turn message off when player left the trigger if (obj.tag == "Player") guiOn = false; }
        if(other.gameObject.name == nombre_objeto_a_colisionar) {
            en_la_zona = false;
        }
    }
}
