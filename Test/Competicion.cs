using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Competicion : MonoBehaviour
{
    public Image Image1;
    public Button B1;
    public Button B2;
    public Text text1;
    public Text text2;
    public Text text1punt;
    public Text text2punt;

    private float timer, timer2;
    private float tiempo_end;
    private bool cambio;
    private bool pulsaB1;
    private bool pulsaB2;
    private bool pausa;
    private bool comienza;
    private bool continua;
    private int nivel;
    private int punt1;
    private int punt2;

    // Cuenta atrás
    private float StartTime = 10;
    float TimerControl, TimerControl2;
    float segs, segs2;
    string segsString, segsString2;
    float milisegs, milisegs2;
    string millsegsString, millsegsString2;
    string TimerString, TimerString2;

    //Celeridad
    int cnt1 = 0;
    int cnt2 = 0;

    void Start()
    {
        timer = 0;
        punt1 = 0;
        punt2 = 0;
        cambio = false;
        pulsaB1 = false;
        pulsaB2 = false;
        pausa = false;
        comienza = false;
        continua = false;
        tiempo_end = Random.Range(1.0f, 8.0f);
        nivel = 1;
        text1.fontSize = 80;
        text2.fontSize = 80;
        text1.text = "Primera ronda: reacción a cambio de pantalla.\nSegunda ronda: cuenta atrás\nFinal: celeridad.\n\nPulse continúa";
        text2.text = "Primera ronda: reacción a cambio de pantalla.\nSegunda ronda: cuenta atrás\nFinal: celeridad.\n\nPulse continúa";
        
    }


    void Update()
    {
        imprimePunt();
        if (comienza)
        {
            continua = false;
            
            if (nivel == 1)
            {
                nivel1();
            }
            else if(nivel == 2)
            {
                nivel2();
            }
            else if(nivel == 3)
            {
                nivel3();
            }
            else if(nivel == 4)
            {
                if (punt1 > punt2)
                {
                    text1.text = "¡Justo ganador!";
                    text2.text = "¿De verdad te dejas perder?";
                    text1.color = new Color(0, 255, 0);
                    text2.color = new Color(255, 0, 0);
                }
                else
                {
                    text2.text = "¡Justo ganador!";
                    text1.text = "¿De verdad te dejas perder?";
                    text1.color = new Color(255, 0, 0);
                    text2.color = new Color(0, 255, 0);
                }
                nivel++;
                comienza = false;
            }
            else
            {
                SceneManager.LoadScene("Multiplayer");
            }

        }

        if (continua && comienza == false)
        {
            comienza = true;
            timer = 0;
            cambio = false;
            Image1.sprite = Resources.Load<Sprite>("Imagenes/competi 1");
            text1.color = new Color(255, 0, 0);
            text2.color = new Color(255, 0, 0);
            pausa = false;
            text1.text = null;
            text2.text = null;
            Thread.Sleep(200);
            pulsaB1 = false;
            pulsaB2 = false;
            continua = false;
        }

    }

    public void PulsoB1()
    {
        pulsaB1 = true;
    }

    public void PulsoB2()
    {
        pulsaB2 = true;
    }

    public void PulsoContinua()
    {
        text1.fontSize = 120;
        text2.fontSize = 120;
        continua = true;
    }
    void imprimePunt()
    {
        text1punt.text = punt1.ToString();
        text2punt.text = punt2.ToString();
    }

    void nivel1()
    {
        timer += Time.deltaTime;
        if (timer > tiempo_end && cambio == false && pausa == false)
        {
            Image1.sprite = Resources.Load<Sprite>("Imagenes/CambioCompeti 1");
            timer = 0;
            cambio = true;
        }

        if (pulsaB1 == true && pulsaB2 == false)
        {
            if (cambio && pausa == false)
            {
                text1.text = "VICTORIA";
                text2.text = "DERROTA";
                punt1++;
                nivel++;
                comienza = false;
            }
            else
            {
                text1.text = "DERROTA";
                text2.text = "VICTORIA";
                punt2++;
                nivel++;
                comienza = false;
            }

        }

        if (pulsaB2 == true && pulsaB1 == false)
        {
            if (cambio && pausa == false)
            {
                text2.text = "VICTORIA";
                text1.text = "DERROTA";
                punt2++;
                nivel++;
                comienza = false;
            }
            else
            {
                text2.text = "DERROTA";
                text1.text = "VICTORIA";
                punt1++;
                nivel++;
                comienza = false;
            }
        }
    }

    void nivel2()
    {
        if (!pulsaB1)
        {
            timer += Time.deltaTime;
            TimerControl = StartTime - timer;
            milisegs = ((TimerControl * 100) % 60);
            segs = (TimerControl % 60);
            segsString = segs.ToString("0");
            millsegsString = milisegs.ToString("00");
            TimerString = string.Format("{0}", segsString);
        }
        if (!pulsaB2)
        {
            timer2 += Time.deltaTime;
            TimerControl2 = StartTime - timer2;
            segs2 = (TimerControl2 % 60);
            milisegs2 = ((TimerControl2 * 100) % 60);
            segsString2 = segs.ToString("0");
            millsegsString2 = milisegs2.ToString("00");
            TimerString2 = string.Format("{0}", segsString2);
        }


        comprobacion();

        if (pulsaB1 == true)
        {
            TimerString = string.Format("{0}:{01}", segsString, millsegsString);
            text1.text = TimerString.ToString();
        }
        else if (TimerControl < 3.0f) // Decidimos ocultar el crono
        {
            text1.text = null;
        }
        else // Imprimimos siempre el crono.
        {
            text1.text = TimerString.ToString();
        }

        if (pulsaB2 == true)
        {
            TimerString2 = string.Format("{0}:{01}", segsString2, millsegsString2);
            text2.text = TimerString2.ToString();
        }
        else if (TimerControl < 3.0f) // Decidimos ocultar el crono
        {
            text2.text = null;
        }
        else // Imprimimos siempre el crono.
        {
            text2.text = TimerString2.ToString();
        }
    }
    void comprobacion()
    {
        if(pulsaB1 && pulsaB2)
        {
            if (Mathf.Abs(segs) < Mathf.Abs(segs2))
            {
                text1.color = new Color(0, 255, 0);
                nivel++;
                punt1++;
            }
            else if (Mathf.Abs(segs2) < Mathf.Abs(segs))
            {
                text2.color = new Color(0, 255, 0);
                punt2++;
                nivel++;
            } // Si son iguales nos fijamos en los millisecs
            else if (Mathf.Abs(milisegs) < Mathf.Abs(milisegs2))
            {
                text1.color = new Color(0, 255, 0);
                nivel++;
                punt1++;
            }
            else if (Mathf.Abs(milisegs2) < Mathf.Abs(milisegs))
            {
                text2.color = new Color(0, 255, 0);
                punt2++;
                nivel++;
            }
            else   //Empate
            {
                if(punt1 > 0)
                {
                    punt2++;
                    text2.color = new Color(0, 255, 0);
                    nivel++;
                }
                else
                {
                    punt1++;
                    text1.color = new Color(0, 255, 0);
                    nivel++;
                }
            }
            comienza = false;
            
        }
    }

    void nivel3()
    {
        timer += Time.deltaTime;
        if (!cambio)
        {
            text1.fontSize = 80;
            text2.fontSize = 80;
            text1.text = "Pulsa la pantalla tantas veces como puedas";
            text2.text = "Pulsa la pantalla tantas veces como puedas";
        }
        if (timer > 10)
        {
            text1.fontSize = 120;
            text2.fontSize = 120;
            cambio = true;
            if (cnt1 > cnt2)
            {
                text1.color = new Color(0, 255, 0);
                punt1++;
                nivel++;
                comienza = false;
            }
            else
            {
                text2.color = new Color(0, 255, 0);
                punt2++;
                nivel++;
                comienza = false;
            }
            text1.text = cnt1.ToString() + " pulsaciones";
            text2.text = cnt2.ToString() + " pulsaciones";
        }
        if (pulsaB1 && cambio == false)
        {
            cnt1++;
            pulsaB1 = false;
        }

        if (pulsaB2 && cambio == false)
        {
            cnt2++;
            pulsaB2 = false;
        }
    }
}
