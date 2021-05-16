using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Auditivo2 : MonoBehaviour
{
    public Text texto;
    public AudioSource audioPlayer;
    public AudioClip sound;
    public Text number;

    private float tiempo_end;
    private float timer;
    private float t_stop;

    bool pausa;
    bool empieza;
    bool empSTT;

    int i;
    float media;
    float min, max;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        media = 0;
        min = 100;
        max = 0;
        i = 0;
        timer = 0;
        pausa = false;
        empieza = false;
        texto.text = "Cuando se pare la música, pulsa la pantalla. \n\nPulse con 2 dedos para comenzar o diga 'empieza'. ";
        audioPlayer.clip = sound;
        empSTT = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (empieza)
        {
            timer += Time.deltaTime;

            if (timer > tiempo_end && pausa == false)
            {
                audioPlayer.Pause();
                pausa = true;
            }

            if ((Input.touchCount > 0 || Input.GetKeyDown("space")) && pausa)
            {
                t_stop = timer - tiempo_end;
                PlayerPrefs.SetFloat("Iteración" + i.ToString(), t_stop);
                timer = 0;
                tiempo_end = Random.Range(5.0f, 10.0f);
                if (i == 7)
                {
                    number.text = null;
                    empieza = false;
                    Resultados();
                    texto.fontSize = 65;
                    texto.text = ("Media de reacción: " + media.ToString("F2") + " seg. \n\nMejor tiempo de reacción: " + min.ToString("F2") + " seg. \n\nPeor tiempo de reacción: " + max.ToString("F2") + " seg.");
                    // Set the score
                    float highscore = PlayerPrefs.GetFloat("HighScore_Music", 0);
                    if (t_stop < highscore || highscore == 0)
                    {
                        PlayerPrefs.SetFloat("HighScore_Music", t_stop);
                    }
                    i = 0;
                }
                else
                {
                    i++;
                    number.text = "Restantes: " + (8 - i).ToString();
                    audioPlayer.Play();
                    timer = 0;
                    pausa = false;
                }
                
                Thread.Sleep(400);
                
            }
        }

        if ((Input.touchCount > 1 || Input.GetKeyDown("a") || empSTT) && empieza == false)
        {
            number.text = "Restantes: " + (8 - i).ToString();
            audioPlayer.Play();
            empieza = true;
            pausa = false;
            timer = 0;
            tiempo_end = Random.Range(5.0f, 10.0f);
            texto.text = null;
            empSTT = false;
        }

    }
    void Resultados()
    {
        for (int j = 0; j <= i; j++)
        {
            if (PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0) < min)
            {
                min = PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
            }
            if (PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0) > max)
            {
                max = PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
            }
            media += PlayerPrefs.GetFloat("Iteración" + j.ToString(), 0);
        }
        media = media / (i + 1);
    }

    public void empiezaSTT()
    {
        empSTT = true;
    }
}
