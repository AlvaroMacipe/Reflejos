using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Auditivo3 : MonoBehaviour
{
    public Text texto;
    public AudioSource audioPlayer;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;
    public AudioClip sound6;
    public AudioClip sound7;
    public AudioClip sound8;
    public AudioClip sound9;
    public AudioClip sound10;
    string[] reconocido = new string[] { "no", "no", "no", "no", "no", "no", "no", "no", "no", "no" };
    public Text number;

    private float tiempo_end;
    private float timer;

    bool pausa;
    bool empieza;
    bool empSTT;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        i = 0;
        timer = 0;
        pausa = false;
        empieza = false;
        empSTT = false;
        audioPlayer.clip = sound1;
    }

    // Update is called once per frame
    void Update()
    {
        if (empieza)
        {
            timer += Time.deltaTime;
            if (timer > tiempo_end && pausa == false)
            {
                audioPlayer.Play();
                pausa = true;
            }

            if ((Input.touchCount > 0 || Input.GetKeyDown("space")) && pausa)
            {
                reconocido[i] = "si";
                timer = 0;
                tiempo_end = Random.Range(3.0f, 4.0f);
                if (i == 0)
                {
                    audioPlayer.clip = sound2;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 1)
                {
                    audioPlayer.clip = sound3;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 2)
                {
                    audioPlayer.clip = sound4;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 3)
                {
                    audioPlayer.clip = sound5;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 4)
                {
                    audioPlayer.clip = sound6;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 5)
                {
                    audioPlayer.clip = sound7;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 6)
                {
                    audioPlayer.clip = sound8;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 7)
                {
                    audioPlayer.clip = sound9;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 8)
                {
                    audioPlayer.clip = sound10;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else
                {
                    texto.text = "RESULTADOS\n\n 40 Hz " + reconocido[0] + "\n" + "80 Hz " + reconocido[1] + "\n" + "160 Hz " + reconocido[2]
                        + "\n" + "320 Hz " + reconocido[3] + "\n" + "640 Hz " + reconocido[4] + "\n" + "1280 Hz " + reconocido[5] + "\n" + "2560 Hz " + reconocido[6]
                        + "\n" + "5120 Hz " + reconocido[7] + "\n" + "10240 Hz " + reconocido[8] + "\n" + "~20000 Hz " + reconocido[9];
                    empieza = false;
                }


            Thread.Sleep(400);
            }

            

            else if(timer > tiempo_end + 2)
            {
                if (i == 0)
                {
                    audioPlayer.clip = sound2;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if(i == 1)
                {
                    audioPlayer.clip = sound3;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 2)
                {
                    audioPlayer.clip = sound4;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 3)
                {
                    audioPlayer.clip = sound5;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 4)
                {
                    audioPlayer.clip = sound6;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 5)
                {
                    audioPlayer.clip = sound7;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 6)
                {
                    audioPlayer.clip = sound8;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 7)
                {
                    audioPlayer.clip = sound9;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else if (i == 8)
                {
                    audioPlayer.clip = sound10;
                    i++;
                    timer = 0;
                    pausa = false;
                }
                else
                {
                    texto.text = "RESULTADOS\n\n 20 Hz " + reconocido[0] + "\n" + "40 Hz " + reconocido[1] + "\n" + "80 Hz " + reconocido[2] + "\n" + "160 Hz " + reconocido[3]
                                + "\n" + "320 Hz " + reconocido[4] + "\n" + "640 Hz " + reconocido[5] + "\n" + "1280 Hz " + reconocido[6] + "\n" + "2560 Hz " + reconocido[7]
                                + "\n" + "5120 Hz " + reconocido[8] + "\n" + "10240 Hz " + reconocido[9] + "\n" + "~20000 Hz " + reconocido[10];
                    empieza = false;
                }

            }
        }

        if ((Input.touchCount > 1 || Input.GetKeyDown("a") || empSTT) && empieza == false)
        {
            empSTT = false;
            empieza = true;
            pausa = false;
            timer = 0;
            tiempo_end = Random.Range(3.0f, 4.0f);
            texto.text = "Pulse la pantalla si escucha algún sonido\n (No se asuste si no escucha nada los primeros 15 segundos)";
        }

    }

    public void empiezaSTT()
    {
        empSTT = true;
    }
}
