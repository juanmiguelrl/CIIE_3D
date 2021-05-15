using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemInfo : MonoBehaviour
{
    public static GameSystemInfo Instance { get; private set; }
    
    public Text TimerText;
    public Text ScoreText;

    public Text ReparacionText;
    public Text ObjetivoText;
    public GameObject Objetivo;
    public GameObject reparacion;
    
    void Awake()
    {
        Instance = this;
    }

    public void UpdateTimer(float time)
    {
        TimerText.text = time.ToString("N1");
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    //funciones añadidas a partir de aquí
    public void MostrarReparacion()
    {
        reparacion.SetActive(true);
    }
    public void UpdateReparacion(float porcentaje)
    {
        ReparacionText.text = porcentaje.ToString("N1") + " %";
    }

    public void MostrarObjetivo()
    {
        Objetivo.SetActive(true);
    }
    public void UpdateObjetivo(string textoObjetivo)
    {
        ObjetivoText.text = textoObjetivo;
    }


}
