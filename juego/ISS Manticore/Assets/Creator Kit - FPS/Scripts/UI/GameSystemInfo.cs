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

    public void UpdateReparacion(float score)
    {
        ReparacionText.text = score.ToString();
    }
}
