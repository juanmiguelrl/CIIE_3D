using UnityEngine;

public class LeverSystem : MonoBehaviour
{
    public GameObject[] levers;

    public GameObject door;
    public GameSystem sistema;

    public string objetivoActivarPalancas;
    public string objetivoPalancasActivadas;

    bool[] leversOpened;

    void Start()
    {
        leversOpened = new bool[levers.Length];
        for (int i = 0; i < leversOpened.Length; i++)
        {
            leversOpened[i] = false;
        }
        sistema.MostrarObjetivo();
        sistema.UpdateObjetivo(objetivoActivarPalancas);
    }

    void openDoor()
    {
        Destroy(door);
    }

    public void checkLevers(GameObject go, bool state)
    {
        int opened = 0;

        for (int i = 0; i < levers.Length; i++)
        {
            if (go == levers[i]) leversOpened[i] = state;
        }

        for (int i = 0; i < leversOpened.Length; i++)
        {
            if (leversOpened[i]) opened++;

            if (opened == levers.Length) {
                sistema.UpdateObjetivo(objetivoPalancasActivadas);
                openDoor();
            }
        }
    }
}
