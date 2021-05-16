using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Auditivo : MonoBehaviour
{
    public Text texto;
    public AudioSource audioPlayer;
    public AudioClip sound;
    public Image altavoz;
    public Text number;

    private float tiempo_end;
    private float timer;
    private float t_stop;

    private bool acierto;
    private bool empieza;
    private bool empSTT;

    private int i;
    private float media;
    private float min, max;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        empieza = false;
        media = 0;
        min = 100;
        max = 0;
        i = 0;
        empSTT = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (empieza)
        {
            timer += Time.deltaTime;

            if (timer > tiempo_end && acierto == false)
            {
                audioPlayer.clip = sound;
                audioPlayer.Play();
                acierto = true; // acierto
            }

            if ((Input.touchCount > 0 || Input.GetKeyDown("space")) && acierto)
            {
                acierto = false;
                t_stop = timer - tiempo_end;
                PlayerPrefs.SetFloat("Iteración" + i.ToString(), t_stop);
                timer = 0;
                tiempo_end = Random.Range(2.0f, 5.0f);
                if (i == 7)
                {
                    number.text = null;
                    empieza = false;
                    Resultados();
                    texto.fontSize = 65;
                    texto.text = ("Media de reacción: " + media.ToString("F2") + " seg. \n\nMejor tiempo de reacción: " + min.ToString("F2") + " seg. \n\nPeor tiempo de reacción: " + max.ToString("F2") + " seg.");
                    // Set the score
                    float highscore = PlayerPrefs.GetFloat("HighScore_Beep", 0);
                    if (min < highscore || highscore == 0)
                    {
                        PlayerPrefs.SetFloat("HighScore_Beep", min);
                    }
                    i = 0;
                }
                else
                {
                    i++;
                    number.text = "Restantes: " + (8 - i).ToString();
                }
                Thread.Sleep(400);
            }

        }// Fin start

        if ((Input.touchCount > 1 || Input.GetKeyDown("a") || empSTT) && empieza == false) // Para empezar por primera vez.
        {
            number.text = "Restantes: " + (8 - i).ToString();
            empieza = true;
            acierto = false;
            timer = 0;
            tiempo_end = Random.Range(2.0f, 5.0f);
            texto.text = null;
            altavoz.color = new Color(0, 0, 0, 0);
            empSTT = false;
        } 
        
    }
    void Resultados()
    {
        for(int j = 0; j <= i; j++)
        {
            if(PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0) < min)
            {
                min = PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
            }
            if(PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0) > max)
            {
                max = PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
            }
            media += PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
        }
        media = media / (i + 1);
    }

    public void SonidoPrueba()
    {
        audioPlayer.clip = sound;
        audioPlayer.Play();
        Thread.Sleep(200);
    }

    public void empiezaSTT()
    {
        empSTT = true;
    }
}
