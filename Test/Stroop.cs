using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Stroop : MonoBehaviour
{
    private const float WAIT_FOR_SERVICES_TIMEOUT_SEC = 5.0f;
    readonly string[] listaColores = new string[] { "ROJO", "VERDE", "AZUL", "AMARILLO", "BLANCO", "FUCSIA", "CIAN" };
    readonly Color[] lista = new Color[] { new Color(255, 0, 0), new Color(0, 255, 0), new Color(0, 0, 255), new Color(255, 255, 0), new Color(255, 255, 255), new Color(255, 0, 255), new Color(0, 255, 255) };
    public Text texto;
    public Text textoExp;
    public Text textoExp2;
    public WatsonSpeechToText SpeechToText;

    private bool empieza;
    private string palabra;
    private int i, j;

    private float timer;
    private int aciertos;

    private bool fin;
    private void Start()
    {
        palabra = "";
        j = Random.Range(0, listaColores.Length - 1);
        i = Random.Range(0, lista.Length - 1);
        timer = 0.0f;
        aciertos = 0;
        fin = false;
        WaitForServicesReady();
        empieza = true;
    }

    private void Update()
    {
        if (empieza && Input.anyKey)
        {
            textoExp.text = null;
            textoExp2.text = null;
            StartTalking();
            empieza = false;
            texto.fontSize = 180;
            texto.text = listaColores[j];
            texto.color = lista[i];
        }

        if (!empieza)
        {
            timer += Time.deltaTime;
        }

        if (empieza == false && timer < 45)
        {
            if (palabra.Contains(listaColores[i]))
            {
                j = Random.Range(0, listaColores.Length - 1);
                i = Random.Range(0, lista.Length - 1);
                texto.text = listaColores[j];
                texto.color = lista[i];
                StartTalking();
                aciertos++;
            }
            else
            {
                StartTalking();
            }
        }
        else if(timer > 45)
        {
            texto.fontSize = 140;
            texto.text = "Puntuación: " + aciertos + "\n\nPulse con dos dedos para volver a empezar.";
            fin = true;
        }

        if((Input.GetButtonDown("a") || Input.touchCount > 1) && fin)
        {
            empieza = true;
            palabra = "";
            j = Random.Range(0, listaColores.Length - 1);
            i = Random.Range(0, lista.Length - 1);
            texto.fontSize = 80;
            timer = 0.0f;
            aciertos = 0;
            fin = false;
        }
    }
    void OnEnable()
    {
        SpeechToText.SpeechRecognized += SpeechToTextSpeechRecognized;
        SpeechToText.StoppedListening += OnSpeechToTextStop;
    }

    void OnDisable()
    {
        SpeechToText.SpeechRecognized -= SpeechToTextSpeechRecognized;
        SpeechToText.StoppedListening -= OnSpeechToTextStop;

        StopAllCoroutines();
    }

    private void OnSpeechToTextStop()
    {
        StopTalking();
    }

    public void StartTalking()
    {
        if (!SpeechToText.IsTalking())
        {
            SpeechToText.StartTalking();
        }
    }

    public void StopTalking()
    {
        if (SpeechToText.IsTalking())
        {
            SpeechToText.StopTalking();
        }
    }

    private void SpeechToTextSpeechRecognized(string text, double confidence, bool final)
    {
        if (final)
        {
            palabra = text.ToUpper();
        }
    }

    IEnumerator WaitForServicesReady()
    {
        float startWaitTime = Time.time;
        bool allServicesReady = false;



        while (Time.time - startWaitTime < WAIT_FOR_SERVICES_TIMEOUT_SEC)
        {
            yield return null;

            if (SpeechToText.IsReady)
            {
                allServicesReady = true;
                break;
            }
        }


        if (allServicesReady)
        {
            StartTalking();
        }
    }



}
