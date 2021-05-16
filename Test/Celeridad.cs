using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Celeridad : MonoBehaviour
{
    public Image circulo;
    public Text texto;
    public Text pulsados;
    public Text boton;
    public Text pulsados2;
    int veces;
    int escala;
    int i;
    float timer;
    float TimerControl;
    float StartTime = 20f;
    string segs, TimerString;
    bool fin;
    bool start;
    
    void Start()
    {
        start = false;
        veces = 0;
        escala = 10;
        i = 1;
        timer = 0;
        segs = "null";
    }
    void Update()
    {
        if (start)
        {
            if (!segs.Equals("00"))
            {
                timer += Time.deltaTime;
                TimerControl = StartTime - timer;
                segs = (TimerControl % 60).ToString("00");
                TimerString = string.Format("{00}", segs);
                texto.text = TimerString;
                pulsados.text = veces.ToString();
            }
            else
            {
                fin = true;
                texto.text = "¡Tiempo!";
                boton.text = null;
                pulsados.text = null;
                pulsados2.text = "Has pulsado " + veces.ToString() + " veces.";
                // Set the score
                float highscore = PlayerPrefs.GetFloat("HighScore_Celeridad", 0);
                if (veces > highscore || highscore == 0)
                {
                    PlayerPrefs.SetFloat("HighScore_Celeridad", veces);
                }
            }

            if (veces > escala)
            {
                i++;
                circulo.transform.localScale = new Vector2(i, i);
                escala *= 2;
            }
        }
        
    }

    public void PulsoB1()
    {
        start = true;
        if (!fin)
        {
            veces++;
        }

    }
}
