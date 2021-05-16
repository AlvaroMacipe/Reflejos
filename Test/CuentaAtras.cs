using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CuentaAtras : MonoBehaviour
{
    private float StartTime;
    float TimerControl;
    string segs;
    float segs2;
    float milisegs2;
    string milisegs;
    string TimerString;
    public Text miTexto;
    public Text miTexto2;
    public Text miTextoNivel;
    public Text TResponse;
    public Text explicacion;
    int limit;
    int nivel;
    float tiempoDeRespuesta;
    private bool pulsado;
    private bool parado;
    private bool nomas;
    private bool acierto;
    private bool empieza;
    private bool STTStart;

    float timer = 0;

    void Start()
    {
        StartTime = 9;
        string edad = PlayerPrefs.GetString("Edad", null);
        if (edad.Equals("Joven"))
        {
            tiempoDeRespuesta = 30f;
        }else if (edad.Equals("Adulto"))
        {
            tiempoDeRespuesta = 40f;
        }
        else
        {
            tiempoDeRespuesta = 60f;
        }
        TimerControl = 0;
        limit = 3;
        pulsado = false;
        parado = false;
        nomas = false;
        acierto = false;
        miTexto.text = null;
        nivel = 1;
        STTStart = false;
    }
    void Update()
    {
        if (empieza)
        {
            TResponse.text = "Admite un desvío de " + tiempoDeRespuesta / 100f + " seg";
            if (pulsado == false)
            {
                timer += Time.deltaTime;
                TimerControl = StartTime - timer;
            }

            milisegs2 = ((TimerControl * 100) % 60);
            segs2 = (TimerControl % 60);
            segs = (TimerControl % 60).ToString("0");
            milisegs = ((TimerControl * 100) % 60).ToString("00");

            TimerString = string.Format("{0}", segs);

            miTextoNivel.text = "Nivel " + nivel;
            if (Input.touchCount == 1 || Input.GetButtonDown("space")) // Si pulsamos la pantalla paramos el crono y lo mostramos por pantalla.
            {
                empieza = false;
                parado = true;
                if (!parado)
                {
                    miTexto.text = TimerString.ToString();
                }
                pulsado = true;
                if (segs.Equals("0") && Mathf.Abs(milisegs2) < tiempoDeRespuesta)
                {
                    acierto = true;
                    if (nivel == 3)
                    {
                        miTexto2.text = "Has superado la prueba, ¡pero puedes seguir mejorando!";
                        TimerString = string.Format("{0}:{01}", segs, milisegs);
                        miTexto.text = TimerString.ToString();
                        miTexto.color = new Color(0, 255, 0);
                        miTexto2.color = new Color(0, 255, 0);
                        nomas = true;
                    }
                    else
                    {
                        miTexto2.text = "¡Simplemente fantastico! \nPulse con dos dedos para pasar al siguiente nivel";
                        TimerString = string.Format("{0}:{01}", segs, milisegs);
                        miTexto.text = TimerString.ToString();
                        miTexto.color = new Color(0, 255, 0);
                    }
                }
                else
                {
                    miTexto2.text = "Para volver a realizar el test pulse con dos dedos";
                    TimerString = string.Format("{0}:{01}", segs, milisegs);
                    miTexto.text = TimerString.ToString();
                    miTexto.color = new Color(255, 0, 0);
                }
            // Set the score
            float highscoreSeg = PlayerPrefs.GetFloat("HighScore_CuentaAtrasSeg", 0);
            if (Mathf.Abs(segs2) < highscoreSeg || highscoreSeg == 0)
            {
                PlayerPrefs.SetFloat("HighScore_CuentaAtrasSeg", Mathf.Abs(segs2));
                float highscoreMill = PlayerPrefs.GetFloat("HighScore_CuentaAtrasMill", 0);
                if (Mathf.Abs(milisegs2) < highscoreMill || highscoreMill == 0)
                {
                    PlayerPrefs.SetFloat("HighScore_CuentaAtrasMill", Mathf.Abs(milisegs2));
                }
            }
            }
            else if (TimerControl < limit && pulsado == false) // Decidimos ocultar el crono
            {
                miTexto.text = null;
            }
            else // Imprimimos siempre el crono.
            {
                if (!parado)
                {
                    miTexto.text = TimerString.ToString();
                }

            }
        }
        

        if((Input.touchCount > 1 || Input.GetKeyDown("a")) && empieza == false)
        {
            explicacion.text = null;
            Thread.Sleep(200);
            if (!nomas)
            {
                if (acierto)
                {
                    nivel++;
                    limit += 2;
                }
                acierto = false;
                pulsado = false;
                parado = false;
                miTexto.text = null;
                miTexto.color = new Color(255, 255, 255);
                TimerControl = 0;
                StartTime = 10;
                timer = 0;
                miTexto2.text = null;
                empieza = true;
            }
        }
    }

    public void speechToStart()
    {
        STTStart = true;
    }
}
