using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CarTimer : MonoBehaviour
{
    public Image ImageStop;
    public Image ImageContinue;
    public VideoClip videoClip;
    public Text texto;
    public Text horizontal;

    private float timer;
    private float umbral;
    private bool empieza;
    private bool pulsado;

    private int num;

    VideoPlayer videoPlayer;

    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;

        ImageStop.color = new Color(0, 0, 0, 0); // Pongo la imagen con alpha = 0. Para que pasemos al vídeo.
        ImageContinue.color = new Color(0, 0, 0, 0); // Pongo la imagen con alpha = 0. Para que pasemos al vídeo.
        texto.text = "Pulse la pantalla (dos dedos) para empezar. \n ¡Atento al STOP! \nCuando aparezca, pulse la pantalla.";
    }

    // Update is called once per frame
    void Update()
    {
        

        if (empieza)
        {
            timer += Time.deltaTime;
            if(timer > umbral)
            {
                if(num == 1)
                    ImageStop.color = new Color(255, 255, 255, 255);
                else
                    ImageContinue.color = new Color(255, 255, 255, 255);
            }

            if (pulsado == false) {
                if(timer > umbral && (Input.touchCount == 1 || Input.GetButtonDown("space")) && num == 1)
                {
                    empieza = false;
                    float dur = ((timer - umbral) * 1000.0f - 50.899f) / 10.947f;
                    float durtim = timer - umbral;
                    texto.text = ("Has tardado: " + durtim.ToString("F2") + " seg \n Tienes: " + dur.ToString("F0") + " años. \n Si no es así, vuelvelo a intentar ;)");
                    videoPlayer.Pause();
                    RenderTexture.active = null;
                    pulsado = true;

                    // Set the score
                    float highscore = PlayerPrefs.GetFloat("HighScore", 0);
                    if (durtim < highscore || highscore == 0)
                    {
                        PlayerPrefs.SetFloat("HighScore", durtim);
                    }
                }
                else if(timer < umbral && (Input.touchCount == 1 || Input.GetButtonDown("space")) && timer > 0.2) // Timer = 0.2 para tener un márgen de seguridad.
                {
                    empieza = false;
                    texto.text = ("¡Muy rápido vaquero!");
                    videoPlayer.Pause();
                    RenderTexture.active = null;
                    pulsado = true;
                }
                else if(timer > umbral && (Input.touchCount == 1 || Input.GetButtonDown("space")) && num == 2)
                {
                    empieza = false;
                    texto.text = ("Quizá no era la señal que esperabas... \n Pulse con dos dedos para volver a empezar");
                    videoPlayer.Pause();
                    RenderTexture.active = null;
                    pulsado = true;
                }
                else if(timer > umbral + 4.0f && num == 2)
                {
                    empieza = false;
                    texto.text = ("Muy bien, no has caido en la trampa... por ahora. \n Pulse con dos dedos para volver a empezar");
                    videoPlayer.Pause();
                    RenderTexture.active = null;
                    pulsado = true;
                }
                else if(timer > umbral + 10.0f && num == 1)
                {
                    empieza = false;
                    texto.text = ("Nos vemos en los judgados. \n Pulse con dos dedos para volver a empezar");
                    videoPlayer.Pause();
                    RenderTexture.active = null;
                    pulsado = true;
                }
            }
        }

        if ((Input.touchCount > 1 || Input.GetButtonDown("a")) && empieza == false)
        {

            videoPlayer.clip = videoClip;
            horizontal.text = null;
            texto.text = null;
            videoPlayer.frame = 0;
            RenderTexture.active = videoPlayer.targetTexture;
            videoPlayer.Play();
            ImageStop.color = new Color(0, 0, 0, 0); // Pongo la imagen con alpha = 0. Para que pasemos al vídeo.
            ImageContinue.color = new Color(0, 0, 0, 0); // Pongo la imagen con alpha = 0. Para que pasemos al vídeo.

            umbral = Random.Range(3.0f, 10.0f);
            num = Random.Range(1, 3);
            // Controlamos el tiempo a partir de ahora.
            empieza = true;
            pulsado = false;
            timer = 0;
            
        }
    }
}
