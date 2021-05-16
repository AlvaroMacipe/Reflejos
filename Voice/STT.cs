using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class STT : MonoBehaviour
{
    private const float WAIT_FOR_SERVICES_TIMEOUT_SEC = 5.0f;
    public AudioSource music;

    public WatsonSpeechToText SpeechToText;

    public GameObject eleccion;
    public GameObject entrenamiento;
    public GameObject auditivo;
    public GameObject resultados;
    public GameObject bienvenida;
    public Edad scriptEdad;
    public Score borrar;
    public Text texto;
    public CuentaAtras start;
    public Auditivo3 start1;
    public Auditivo2 start2;
    public Auditivo start3;

    private string palabra;
    private void Start()
    {
        palabra = "";
        StartCoroutine(WaitForServicesReady());
    }

    private void Update()
    {
        if (SpeechToText.IsReady)
        {
            if (palabra.Contains("PARAR MÚSICA"))
            {
                music.Stop();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("MULTI"))
            {
                SceneManager.LoadScene("Multiplayer");
                palabra = "";
            }
            else if (palabra.Contains("CUENTA ATRÁS"))
            {
                SceneManager.LoadScene("CuentaAtras");
                palabra = "";
            }
            else if (palabra.Contains("FIGURAS"))
            {
                SceneManager.LoadScene("Figuras");
                palabra = "";
            }
            else if (palabra.Contains("PANTALLAS"))
            {
                SceneManager.LoadScene("Pantallas1");
                palabra = "";
            }
            else if (palabra.Contains("COLORES"))
            {
                SceneManager.LoadScene("Colores");
                palabra = "";
            }
            else if (palabra.Contains("CONDUCCIÓN"))
            {
                SceneManager.LoadScene("Conduccion");
                palabra = "";
            }
            else if (palabra.Contains("CELERIDAD"))
            {
                SceneManager.LoadScene("Celeridad");
                palabra = "";
            }
            else if (palabra.Contains("CORTO"))
            {
                SceneManager.LoadScene("Auditivos");
                palabra = "";
            }
            else if (palabra.Contains("CONTINUO"))
            {
                SceneManager.LoadScene("Auditivos2");
                palabra = "";
            }
            else if (palabra.Contains("STROOP"))
            {
                SceneManager.LoadScene("Stroop");
                palabra = "";
            }
            else if (palabra.Contains("ENTRENAMIENTO VISUAL"))
            {
                eleccion.SetActive(false);
                entrenamiento.SetActive(true);
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("ENTRENAMIENTO AUDITIVO"))
            {
                eleccion.SetActive(false);
                auditivo.SetActive(true);
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("RESULTADOS"))
            {
                eleccion.SetActive(false);
                resultados.SetActive(true);
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("JOVEN"))
            {
                bienvenida.SetActive(false);
                eleccion.SetActive(true);
                scriptEdad.Joven();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("ADULTO"))
            {
                bienvenida.SetActive(false);
                eleccion.SetActive(true);
                scriptEdad.Adulto();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("SENIOR"))
            {
                bienvenida.SetActive(false);
                eleccion.SetActive(true);
                scriptEdad.Senior();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("BORRAR"))
            {
                borrar.Borrar();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("VOLVER AL MENÚ"))
            {
                SceneManager.LoadScene("Menu");
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("EMPIEZA"))
            {
                start.speechToStart();
                start1.empiezaSTT();
                start2.empiezaSTT();
                start3.empiezaSTT();
                StartTalking();
                palabra = "";
            }
            else if (palabra.Contains("TEST AUDITIVO"))
            {
                SceneManager.LoadScene("Auditivos3");
                palabra = "";
            }
            else
            {
                StartTalking();
            }
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
            texto.text = palabra;
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
