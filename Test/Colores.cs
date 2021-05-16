using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Colores : MonoBehaviour
{
    public Text colores;
    public Text niveles;
    public Text explicacion;
    readonly string[] listaColores = new string[] {"ROJO", "VERDE", "AZUL", "AMARILLO", "BLANCO", "FUCSIA", "CIAN" };
    int i, j;
    readonly Color[] lista = new Color[] {new Color(255, 0, 0), new Color(0, 255, 0), new Color(0, 0, 255), new Color(255, 255, 0), new Color(255, 255, 255), new Color(255, 0, 255), new Color(0, 255, 255)};
    bool verdad = false;
    bool pausa = false;
    bool ganador = false;
    bool pulsado = false; // Determinar si he pulsado ya una vez o no.
    float tiempo_end;
    float timer;
    float timer2;
    int nivel = 1;
    bool fin = false;
    bool acierto;
    bool comienza;
    float tiempoDeRespuesta;
    public Text TResponse;

    void Start()
    {
        comienza = false;
        string edad = PlayerPrefs.GetString("Edad", null);
        if (edad.Equals("Joven"))
        {
            tiempoDeRespuesta = 60f;
            tiempo_end = 1.5f;
        }
        else if (edad.Equals("Adulto"))
        {
            tiempoDeRespuesta = 70f;
            tiempo_end = 2f;
        }
        else
        {
            tiempoDeRespuesta = 90f;
            tiempo_end = 2.5f;
        }
        verdad = false;
        pausa = false;
        ganador = false;
        acierto = false;
        
        timer = 0;
        timer2 = 0;

    }
    void Update()
    {
        if (comienza)
        {
            niveles.text = "Nivel: " + nivel;
            TResponse.text = "Admite un tiempo de reacción de " + tiempoDeRespuesta / 100f + " seg";
            if (pausa == false)
            {
                timer = 0;
                i = Random.Range(0, listaColores.Length - 1);
                j = Random.Range(0, lista.Length - 1);

                if (i == j)
                {
                    timer2 = 0;
                    verdad = true; // Comprobamos que es cierto.
                }
                else verdad = false;
                colores.fontSize = 300;
                colores.text = listaColores[i];
                print(verdad);
                colores.color = lista[j];
            }
            if (verdad == true)
            {
                timer2 += Time.deltaTime;
            }

            timer += Time.deltaTime;

            if ((Input.touchCount == 1 || Input.GetButtonDown("space")) && verdad == true && pulsado == false)
            {
                comienza = false;
                pausa = true;
                ganador = true;
                pulsado = true;
                float durtim = timer2;
                if (durtim < tiempoDeRespuesta / 100)
                {
                    acierto = true;
                    colores.fontSize = 100;
                    colores.text = "Has tardado: " + durtim.ToString("F2") + " ms \n Pulsa con 2 dedos en la pantalla para pasar al siguiente nivel.";
                    colores.color = new Color(0, 225, 0);

                    if (nivel == 3)
                    {
                        fin = true;
                        colores.text = "Has tardado: " + durtim.ToString("F2") + " ms \n Has alcanzado el nivel máximo, ¡enhorabuena!";
                        colores.color = new Color(0, 225, 255);
                    }
                }
                else
                {
                    colores.fontSize = 100;
                    colores.text = "Has tardado: " + durtim.ToString("F2") + " ms \n Pulsa con 2 dedos en la pantalla para volverlo a intentar.";
                    colores.color = new Color(255, 0, 0);
                }

                // Set the score
                float highscore = PlayerPrefs.GetFloat("HighScore_Colores", 0);
                if (durtim < highscore || highscore == 0)
                {
                    PlayerPrefs.SetFloat("HighScore_Colores", durtim);
                }
            }
            else if ((Input.touchCount == 1 || Input.GetButtonDown("space")) && verdad == false)
            {
                colores.text = "¡NO!";
                colores.color = new Color(255, 0, 0);
            }
            else if (timer >= tiempo_end && ganador == false)
            {
                pausa = false;
            }
            else
            {
                if (!ganador)
                    pausa = true;
            }
        }
        

        if(comienza == false && (Input.touchCount == 2 || Input.GetButtonDown("a")))
        {
            explicacion.text = null;
            if (!fin)
            {
                comienza = true;
                if (acierto)
                {
                    nivel++;
                    tiempo_end -= 0.5f;
                }

                acierto = false;
                pulsado = false;
                verdad = false;
                pausa = false;
                ganador = false;
                timer = 0;
                timer2 = 0;
                Thread.Sleep(200); // Para evitar conflictos entre pulsaciones.
            }
            
        }

        
    }
}
